using ForumSever;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MessagePack;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;

namespace forumTests
{
    
    
    /// <summary>
    ///This is a test class for LogicManagerTest and is intended
    ///to contain all LogicManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogicManagerTest {


        private TestContext testContextInstance;
        private LogicManager lg;

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

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }


        [TestInitialize]
        public void Initialize() {
            lg = new LogicManager();
            for (int i = 0; i < 3; i++) {
                MemberInfo memb = new MemberInfo("utest"+i, "ftest", "itest", "ptest", "bla", "bla", "bla", "etest", "bla", "0");
                lg.register(memb);
            }
        }

        [TestCleanup]
        public void Terminate() {
            for (int i = 0; i < 3; i++) {
                lg.db.runSelectSQL("Delete From Users where username = 'utest"+i+"'");
                lg.db.closeconn();
                lg.db.runSelectSQL("DROP TABLE utest"+i);
                lg.db.closeconn();
            }
        }

        /// <summary>
        ///A test for LogicManager Constructor
        ///</summary>
        [TestMethod()]
        public void LogicManagerConstructorTest() {
            Database p_db = null; // TODO: Initialize to an appropriate value
            LogicManager target = new LogicManager(p_db);
            Assert.IsNotNull(target.usersIp);
            Assert.IsNotNull(target.usersData);
        }

        /// <summary>
        ///A test for LogicManager Constructor
        ///</summary>
        [TestMethod()]
        public void LogicManagerConstructorTest1() {
            LogicManager target = new LogicManager();
            Assert.IsNotNull(target.db);
            Assert.IsNotNull(target.usersIp);
            Assert.IsNotNull(target.usersData);
        }


        /// <summary>
        ///A test for register
        ///</summary>
        [TestMethod()]
        public void registerTest() {
            MemberInfo memb = new MemberInfo("utest", "ftest", "itest", "ptest", "bla", "bla", "bla", "etest", "bla", "0");
            int actual;
            actual = lg.register(memb);
            Assert.AreEqual(0, actual);
            Assert.IsTrue(lg.db.recordExsist("SELECT * FROM Users WHERE (username = 'utest') and (fname = 'ftest') and (password = '"+lg.db.getMd5Hash("ptest")+"')"));
            lg.db.runSelectSQL("Delete From Users where username = 'utest'");
            lg.db.closeconn();
            lg.db.runSelectSQL("DROP TABLE utest");
            lg.db.closeconn();
        }



        /// <summary>
        ///A test for FindMemberByUser
        ///</summary>
        [TestMethod()]
        public void FindMemberByUserTest() {
            string p_user = "utest";
            MemberInfo expected = new MemberInfo("utest", "ftest", "itest", "ptest", "bla", "bla", "bla", "etest", "bla", "0");
            lg.register(expected);
            expected.setpass(lg.db.getMd5Hash("ptest"));
            MemberInfo actual;
            actual = lg.FindMemberByUser(p_user);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            lg.db.runSelectSQL("Delete From Users where username = 'utest'");
            lg.db.closeconn();
            lg.db.runSelectSQL("DROP TABLE utest");
            lg.db.closeconn();
        }

      /*  /// <summary>
        ///A test for addForum
        ///</summary>
        [TestMethod()]
        public void addForumTest() {
  
            int actual;
            actual = lg.addForum(p_userID, p_topic);
            Assert.AreEqual(0, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }*/

        /// <summary>
        ///A test for addMeAsFriend
        ///</summary>
        [TestMethod()]
        public void addMeAsFriendTest() {
            int actual = lg.addMeAsFriend("utest0", "utest1");
            Assert.AreEqual(0, actual);
            Assert.IsTrue(lg.db.recordExsist("select * from utest0 where uname = 'utest1'"));
        }

   /*     /// <summary>
        ///A test for addPost
        ///</summary>
        [TestMethod()]
        public void addPostTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_tid = 0; // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            int parentId = 0; // TODO: Initialize to an appropriate value
            string p_topic = string.Empty; // TODO: Initialize to an appropriate value
            string p_content = string.Empty; // TODO: Initialize to an appropriate value
            string p_uname = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.addPost(p_tid, p_fid, parentId, p_topic, p_content, p_uname);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for addTread
        ///</summary>
        [TestMethod()]
        public void addTreadTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            string p_uname = string.Empty; // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            string p_topic = string.Empty; // TODO: Initialize to an appropriate value
            string p_content = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.addTread(p_uname, p_fid, p_topic, p_content);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for addUserIP
        ///</summary>
        [TestMethod()]
        public void addUserIPTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string IP = string.Empty; // TODO: Initialize to an appropriate value
            target.addUserIP(userName, IP);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for fillPostTree
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ForumSever.exe")]
        public void fillPostTreeTest() {
            LogicManager_Accessor target = new LogicManager_Accessor(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            int p_tid = 0; // TODO: Initialize to an appropriate value
            PostsTree pt = null; // TODO: Initialize to an appropriate value
            List<Quartet> result = null; // TODO: Initialize to an appropriate value
            string uname = string.Empty; // TODO: Initialize to an appropriate value
            List<PostsTree> expected = null; // TODO: Initialize to an appropriate value
            List<PostsTree> actual;
            actual = target.fillPostTree(p_fid, p_tid, pt, result, uname);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getForum
        ///</summary>
        [TestMethod()]
        public void getForumTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            List<Quartet> expected = null; // TODO: Initialize to an appropriate value
            List<Quartet> actual;
            actual = target.getForum(p_fid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getForumName
        ///</summary>
        [TestMethod()]
        public void getForumNameTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int t_fid = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getForumName(t_fid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getForumViewers
        ///</summary>
        [TestMethod()]
        public void getForumViewersTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int t_fid = 0; // TODO: Initialize to an appropriate value
            List<string> expected = null; // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.getForumViewers(t_fid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getForums
        ///</summary>
        [TestMethod()]
        public void getForumsTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            List<Quartet> expected = null; // TODO: Initialize to an appropriate value
            List<Quartet> actual;
            actual = target.getForums();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }*/

        /// <summary>
        ///A test for getFriends
        ///</summary>
        [TestMethod()]
        public void getFriendsTest() {
            lg.addMeAsFriend("utest0", "utest1");
            lg.addMeAsFriend("utest0", "utest2");
            List<string> actual = lg.getFriends("utest0");
            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains("utest1"));
            Assert.IsTrue(actual.Contains("utest2"));
        }

        /// <summary>
        ///A test for getFriendsToUpdate
        ///</summary>
        [TestMethod()]
        public void getFriendsToUpdateTest() {
            lg.addMeAsFriend("utest0", "utest1");
            lg.addMeAsFriend("utest2", "utest1");
            List<string> actual = lg.getFriendsToUpdate("utest1");
            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains("utest2"));
            Assert.IsTrue(actual.Contains("utest0"));
        }

       /* /// <summary>
        ///A test for getPost
        ///</summary>
        [TestMethod()]
        public void getPostTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            int p_tid = 0; // TODO: Initialize to an appropriate value
            int p_index = 0; // TODO: Initialize to an appropriate value
            string p_uname = string.Empty; // TODO: Initialize to an appropriate value
            ForumPost expected = null; // TODO: Initialize to an appropriate value
            ForumPost actual;
            actual = target.getPost(p_fid, p_tid, p_index, p_uname);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getThread
        ///</summary>
        [TestMethod()]
        public void getThreadTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            int p_tid = 0; // TODO: Initialize to an appropriate value
            ForumThread expected = null; // TODO: Initialize to an appropriate value
            ForumThread actual;
            actual = target.getThread(p_fid, p_tid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getThreadName
        ///</summary>
        [TestMethod()]
        public void getThreadNameTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int t_fid = 0; // TODO: Initialize to an appropriate value
            int t_tid = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getThreadName(t_fid, t_tid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getThreadPosts
        ///</summary>
        [TestMethod()]
        public void getThreadPostsTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            int p_tid = 0; // TODO: Initialize to an appropriate value
            List<Quartet> expected = null; // TODO: Initialize to an appropriate value
            List<Quartet> actual;
            actual = target.getThreadPosts(p_fid, p_tid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getThreadPostsAndContent
        ///</summary>
        [TestMethod()]
        public void getThreadPostsAndContentTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            int p_tid = 0; // TODO: Initialize to an appropriate value
            string uname = string.Empty; // TODO: Initialize to an appropriate value
            PostsTree expected = null; // TODO: Initialize to an appropriate value
            PostsTree actual;
            actual = target.getThreadPostsAndContent(p_fid, p_tid, uname);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getThreadViewersToUpdate
        ///</summary>
        [TestMethod()]
        public void getThreadViewersToUpdateTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            string t_uname = string.Empty; // TODO: Initialize to an appropriate value
            int t_fid = 0; // TODO: Initialize to an appropriate value
            int t_tid = 0; // TODO: Initialize to an appropriate value
            List<string> expected = null; // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.getThreadViewersToUpdate(t_uname, t_fid, t_tid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getUserDataFromIP
        ///</summary>
        [TestMethod()]
        public void getUserDataFromIPTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            string IP = string.Empty; // TODO: Initialize to an appropriate value
            UserData expected = null; // TODO: Initialize to an appropriate value
            UserData actual;
            actual = target.getUserDataFromIP(IP);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getUserFromIP
        ///</summary>
        [TestMethod()]
        public void getUserFromIPTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            string IP = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getUserFromIP(IP);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }*/

        /// <summary>
        ///A test for getUsers
        ///</summary>
        [TestMethod()]
        public void getUsersTest() {
            List<string> actual;
            actual = lg.getUsers("utest2");
            //Assert.AreEqual(3, actual.Count);
            Assert.IsTrue(actual.Contains("utest2"));
            Assert.IsTrue(actual.Contains("utest1"));
            Assert.IsTrue(actual.Contains("utest0"));
        }

      /*  /// <summary>
        ///A test for isForum
        ///</summary>
        [TestMethod()]
        public void isForumTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.isForum(p_fid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }*/

        /// <summary>
        ///A test for isLogged
        ///</summary>
        [TestMethod()]
        public void isLoggedTest() {
            lg.login("utest0", "ptest");
            Assert.IsTrue(lg.isLogged("utest0"));
            Assert.IsFalse(lg.isLogged("utest1"));
        }

        /// <summary>
        ///A test for isMember
        ///</summary>
        [TestMethod()]
        public void isMemberTest() {
            Assert.IsTrue(lg.isMember("utest0"));
            Assert.IsFalse(lg.isMember("utest3"));
        }

        /// <summary>
        ///A test for login
        ///</summary>
        [TestMethod()]
        public void loginTest() {
            lg.login("utest0", "ptest");
            SqlDataReader reader = lg.db.runSelectSQL("SELECT * FROM Users WHERE username = 'utest0'");
            Assert.IsTrue(reader.HasRows);
            reader.Read();
            Assert.AreEqual(Convert.ToInt32(reader["logged"]), 1);
            lg.db.closeconn();
        }

        /// <summary>
        ///A test for logout
        ///</summary>
        [TestMethod()]
        public void logoutTest() {
            lg.login("utest0", "ptest");
            lg.logout("utest0");
            SqlDataReader reader = lg.db.runSelectSQL("SELECT * FROM Users WHERE username = 'utest0'");
            Assert.IsTrue(reader.HasRows);
            reader.Read();
            Assert.AreEqual(Convert.ToInt32(reader["logged"]), 0);
            lg.db.closeconn();
        }


        /// <summary>
        ///A test for removeMeAsFriend
        ///</summary>
        [TestMethod()]
        public void removeMeAsFriendTest() {
            lg.addMeAsFriend("utest0", "utest1");
            int actual = lg.removeMeAsFriend("utest0", "utest1");
            Assert.AreEqual(0, actual);
            Assert.IsFalse(lg.db.recordExsist("select * from utest0 where uname = 'utest1'"));
        }

    /*    /// <summary>
        ///A test for removePost
        ///</summary>
        [TestMethod()]
        public void removePostTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            int p_tid = 0; // TODO: Initialize to an appropriate value
            int p_index = 0; // TODO: Initialize to an appropriate value
            string p_uname = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.removePost(p_fid, p_tid, p_index, p_uname);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for removeThread
        ///</summary>
        [TestMethod()]
        public void removeThreadTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int p_fid = 0; // TODO: Initialize to an appropriate value
            int p_tid = 0; // TODO: Initialize to an appropriate value
            string p_uname = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.removeThread(p_fid, p_tid, p_uname);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for removeUserIP
        ///</summary>
        [TestMethod()]
        public void removeUserIPTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            string IP = string.Empty; // TODO: Initialize to an appropriate value
            target.removeUserIP(IP);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for updateCurrentThread
        ///</summary>
        [TestMethod()]
        public void updateCurrentThreadTest() {
            LogicManager target = new LogicManager(); // TODO: Initialize to an appropriate value
            int t_fid = 0; // TODO: Initialize to an appropriate value
            int t_tid = 0; // TODO: Initialize to an appropriate value
            string t_uname = string.Empty; // TODO: Initialize to an appropriate value
            target.updateCurrentThread(t_fid, t_tid, t_uname);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }*/
    }
}
