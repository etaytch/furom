using ForumSever;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MessagePack;
using Protocol;
using System.Data.SqlClient;

namespace forumTests
{
    
    
    /// <summary>
    ///This is a test class for MassageHandlerTest and is intended
    ///to contain all MassageHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MassageHandlerTest {


        private TestContext testContextInstance;
        private MassageHandler_Accessor target;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup() {
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize() {
            target = new MassageHandler_Accessor(); 
            for(int i=0;i<3;i++){
                target.Handle(new RegisterMessage("utest", "ftest", "utest" + i, "ptest", "bla", "bla", "bla", "etest", "bla", "0"));
                target.Handle(new LoginMessage("utest"+i, "ptest"));
            }
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() {
            for (int i = 0; i < 3; i++) {
                target.lm.db.runSelectSQL("Delete From Users where username = 'utest" + i + "'");
                target.lm.db.closeconn();
                target.lm.db.runSelectSQL("DROP TABLE utest" + i);
                target.lm.db.closeconn();
            }
        }

        #endregion


        /// <summary>
        ///A test for MassageHandler Constructor
        ///</summary>
        [TestMethod()]
        public void MassageHandlerConstructorTest() {
            LogicManager lm = new LogicManager("app");
            MassageHandler target = new MassageHandler(lm);
            Assert.IsNotNull(target.ee);
        }

        /// <summary>
        ///A test for MassageHandler Constructor
        ///</summary>
        [TestMethod()]
        public void MassageHandlerConstructorTest1() {
            MassageHandler target = new MassageHandler();
            Assert.IsNotNull(target.ee);
            Assert.IsNotNull(target.lm);
        }
        
        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest() {
            target.Handle(new AddFriendMessage("utest0", "utest1"));
            target.Handle(new RemoveFriendMessage("utest0", "utest1"));
            Assert.IsFalse(target.lm.db.recordExsist("select * from utest0 where uname = 'utest1'"));
        }
        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest2() {
            target.Handle(new AddFriendMessage("utest0", "utest1"));
            Assert.IsTrue(target.lm.db.recordExsist("select * from utest0 where uname = 'utest1'"));
        }
        
        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest3() {
            target.Handle(new RegisterMessage("ftest", "ftest", "utest", "ptest", "bla", "bla", "bla", "etest", "bla", "bla"));
            Assert.IsTrue(target.lm.db.recordExsist("SELECT * FROM Users WHERE (username = 'utest') and (fname = 'ftest') and (password = '" + target.lm.db.getMd5Hash("ptest") + "')"));
            target.lm.db.runSelectSQL("Delete From Users where username = 'utest'");
            target.lm.db.closeconn();
            target.lm.db.runSelectSQL("DROP TABLE utest");
            target.lm.db.closeconn();
        }

        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest11() {
            target.Handle(new LogoutMessage("utest0"));
            SqlDataReader reader = target.lm.db.runSelectSQL("SELECT * FROM Users WHERE username = 'utest0'");
            Assert.IsTrue(reader.HasRows);
            reader.Read();
            Assert.AreEqual(Convert.ToInt32(reader["logged"]), 0);
            target.lm.db.closeconn();
            
        }
        
        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest12() {
            target.Handle(new RegisterMessage("ftest", "ftest", "utest", "ptest", "bla", "bla", "bla", "etest", "bla", "bla"));
            target.Handle(new LoginMessage("utest", "ptest"));
            SqlDataReader reader = target.lm.db.runSelectSQL("SELECT * FROM Users WHERE username = 'utest'");
            Assert.IsTrue(reader.HasRows);
            reader.Read();
            Assert.AreEqual(Convert.ToInt32(reader["logged"]), 1);
            target.lm.db.closeconn();
            target.lm.db.runSelectSQL("Delete From Users where username = 'utest'");
            target.lm.db.closeconn();
            target.lm.db.runSelectSQL("DROP TABLE utest");
            target.lm.db.closeconn();
        }
    }
}
