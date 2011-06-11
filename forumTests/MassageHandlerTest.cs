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
        }

        #endregion


        /// <summary>
        ///A test for MassageHandler Constructor
        ///</summary>
        [TestMethod()]
        public void MassageHandlerConstructorTest() {
            LogicManager lm = new LogicManager();
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
        /*
        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest1() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            DeletePostMessage t_deletePostMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_deletePostMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
*/
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
        }
        /*
        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest4() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            DeleteThreadMessage t_deleteThreadMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_deleteThreadMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest5() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            GetSystemMessage t_getSystemMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_getSystemMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest6() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            GetForumMessage t_getForumMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_getForumMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest7() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            GetPostMessage t_getPostMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_getPostMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest8() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            GetThreadMessage t_getThreadMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_getThreadMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
       
        
        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest10() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            AddPostMessage t_addPostMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_addPostMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
        */
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
        }
        

        /*
        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest14() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            AddForumMessage t_addForumMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_addForumMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Handle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void HandleTest15() {
            MassageHandler_Accessor target = new MassageHandler_Accessor(); // TODO: Initialize to an appropriate value
            AddThreadMessage t_addThreadMsg = null; // TODO: Initialize to an appropriate value
            target.Handle(t_addThreadMsg);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
        */
    }
}
