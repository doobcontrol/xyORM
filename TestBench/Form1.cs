using xy.Db;
using xy.Db.PostgreSQL;
using xy.ORM;

namespace TestBench
{
    public partial class Form1 : Form
    {
        string dbServer = "localhost";
        string dbName = "testDb";
        string dbUser = "testUser";
        string dbPassword = "testPassword";
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //add all models to model list
            //Company c = Company.i; //
            BaseModel.addToModelList(typeof(Company));
            BaseModel.addToModelList(typeof(Employee));

            //create database
            Dictionary<string, string> dbCreatePars = new Dictionary<string, string>();
            dbCreatePars.Add(DbService.pn_dbServer, dbServer);
            dbCreatePars.Add(DbService.pn_dbName, dbName);
            dbCreatePars.Add(DbService.pn_dbUser, dbUser);
            dbCreatePars.Add(DbService.pn_dbPassword, dbPassword);
            Dictionary< string, string> adminPars = new Dictionary<string, string>();
            string admindbName = "postgres";
            string admindbUser = "postgres";
            string admindbPassword = "123456";
            string admindbServer = "localhost";
            adminPars.Add(DbService.pn_dbServer,
                admindbServer);
            adminPars.Add(DbService.pn_dbName,
                admindbName);
            adminPars.Add(DbService.pn_dbUser,
                admindbUser);
            adminPars.Add(DbService.pn_dbPassword,
                admindbPassword);
            BaseModel.DefaultDbService = new DbService(
                new PostgreSQLDbAccess()
                );
            await BaseModel.CreateDatabaseAsync(
                dbCreatePars,
                adminPars
                );
            
            //use model instance
            Company.i.update();
        }
    }
}
