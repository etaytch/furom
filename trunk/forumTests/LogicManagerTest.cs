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

        private int testForumId;
        private int testThreadId;
        private int testPostId;
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
            lg = new LogicManager("app");
            for (int i = 0; i < 3; i++) {
                MemberInfo memb = new MemberInfo("utest"+i, "ftest", "itest", "ptest", "bla", "bla", "bla", "etest", "bla", "0");
                lg.register(memb);
            }
            lg.db.addForum("forumtest");
            lg.db.closeconn();
            SqlDataReader reader = lg.db.runSelectSQL("SELECT fid FROM forums WHERE fname = 'forumtest'");
            reader.Read();
            testForumId = Convert.ToInt32(reader["fid"]);
            lg.db.closeconn();
            testThreadId = lg.db.addTread(new ForumThread(testForumId, "ttopic", "tcontent", "utest0"));
            lg.db.addPost(testThreadId, testForumId, 0, "tPostTopic", "tPostcontent", "utest0");
            reader = lg.db.runSelectSQL("SELECT pid FROM Posts WHERE subject = 'tPostTopic'");
            reader.Read();
            testPostId = Convert.ToInt32(reader["pid"]);
            lg.db.closeconn();
        }

        [TestCleanup]
        public void Terminate() {
            lg.db.removePost(testForumId, testThreadId, testPostId);
            lg.db.removeThread(testForumId, testThreadId);
            lg.db.removeForum(testForumId);
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
            LogicManager target = new LogicManager(p_db,"app");
            Assert.IsNotNull(target.usersIp);
            Assert.IsNotNull(target.usersData);
        }

        /// <summary>
        ///A test for LogicManager Constructor
        ///</summary>
        [TestMethod()]
        public void LogicManagerConstructorTest1() {
            LogicManager target = new LogicManager("app");
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

        /// <summary>
        ///A test for addMeAsFriend
        ///</summary>
        [TestMethod()]
        public void addMeAsFriendTest() {
            int actual = lg.addMeAsFriend("utest0", "utest1");
            Assert.AreEqual(0, actual);
            Assert.IsTrue(lg.db.recordExsist("select * from utest0 where uname = 'utest1'"));
        }

        /// <summary>
        ///A test for addPost
        ///</summary>
        [TestMethod()]
        public void addPostTest() {
            int expected = lg.addPost(testThreadId, testForumId, 0, "blaTopic", "blaContent", "utest3");
            Assert.AreEqual(expected, -3);
            expected = lg.addPost(-9, testForumId, 0, "blaTopic", "blaContent", "utest0");
            Assert.AreEqual(expected, -6);
            expected = lg.addPost(testThreadId, testForumId, 0, "blaTopic", "blaContent", "utest0");
            Assert.AreEqual(expected, 0);
            SqlDataReader reader = lg.db.runSelectSQL("SELECT pid FROM Posts WHERE subject = 'blaTopic'");
            Assert.IsTrue(reader.HasRows);
            reader.Read();
            int actual = Convert.ToInt32(reader["pid"]);
            lg.db.closeconn();
            Assert.IsTrue(actual>0);
        }

        /// <summary>
        ///A test for addTread
        ///</summary>
        [TestMethod()]
        public void addTreadTest() {
            int expected = lg.addTread("utest3",testForumId, "blaTopic", "blaContent");
            Assert.AreEqual(expected, -3);
            expected = lg.addTread("utest0", testForumId, "ttopic", "blaContent");
            Assert.AreEqual(expected, -5);
            expected = lg.addTread("utest0", testForumId, "blaTopic", "blaContent");
            Assert.IsTrue(expected > 0);
            SqlDataReader reader = lg.db.runSelectSQL("SELECT tid FROM threads WHERE subject = 'blaTopic'");
            Assert.IsTrue(reader.HasRows);
            reader.Read();
            int actual = Convert.ToInt32(reader["tid"]);
            lg.db.closeconn();
            Assert.IsTrue(actual > 0);
        }

        /// <summary>
        ///A test for getForum
        ///</summary>
        [TestMethod()]
        public void getForumTest() {
            List<Quartet> expected = new List<Quartet>();
            expected.Add(new Quartet(testThreadId,0,"ttopic","utest0"));
            List<Quartet> actual = lg.getForum(testForumId);
            Assert.AreEqual(expected[0]._subject, actual[0]._subject);
            Assert.AreEqual(expected[0]._pIndex, actual[0]._pIndex);
            Assert.AreEqual(expected[0]._author, actual[0]._author);
        }

       

        /// <summary>
        ///A test for getForumName
        ///</summary>
        [TestMethod()]
        public void getForumNameTest() {
            string actual = lg.getForumName(testForumId);
            Assert.AreEqual("forumtest", actual);

        }


        /// <summary>
        ///A test for getForums
        ///</summary>
        [TestMethod()]
        public void getForumsTest() {
            List<Quartet> expected = new List<Quartet>();
            expected.Add(new Quartet(testForumId, 0, "forumtest", ""));
            List<Quartet> actual = lg.getForums();
            Assert.AreEqual(expected[0]._subject, actual[0]._subject);
            Assert.AreEqual(expected[0]._pIndex, actual[0]._pIndex);
            Assert.AreEqual(expected[0]._author, actual[0]._author);
           
        }

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

        /// <summary>
        ///A test for getPost
        ///</summary>
        [TestMethod()]
        public void getPostTest() {
            ForumPost expected = new ForumPost(testForumId, testThreadId, 0,"tPostTopic","tPostcontent","utest0"); 
            ForumPost actual = lg.getPost(testForumId, testThreadId, testPostId, "utest0");
            Assert.AreEqual(expected.getAuthor(), actual.getAuthor());
            Assert.AreEqual(expected.getContent(), actual.getContent());
            Assert.AreEqual(expected.getTopic(), actual.getTopic());
        }

        /// <summary>
        ///A test for getThread
        ///</summary>
        [TestMethod()]
        public void getThreadTest() {
            ForumThread expected = new ForumThread(testForumId, "ttopic", "tcontent", "utest0");
            ForumThread actual = lg.getThread(testForumId, testThreadId);
            Assert.AreEqual(expected.getAuthor(), actual.getAuthor());
            Assert.AreEqual(expected.getContent(), actual.getContent());
            Assert.AreEqual(expected.getTopic(), actual.getTopic());
        }

        /// <summary>
        ///A test for getThreadName
        ///</summary>
        [TestMethod()]
        public void getThreadNameTest() {
            string actual = lg.getThreadName(testForumId, testThreadId);
            Assert.AreEqual("ttopic", actual);
        }

        /// <summary>
        ///A test for getThreadPosts
        ///</summary>
        [TestMethod()]
        public void getThreadPostsTest() {
            List<Quartet> expected = new List<Quartet>();
            expected.Add(new Quartet(testPostId,0,"tPostTopic","utest0"));
            List<Quartet> actual = lg.getThreadPosts(testForumId, testThreadId);
            Assert.AreEqual(expected[0]._author, actual[0]._author);
            Assert.AreEqual(expected[0]._subject, actual[0]._subject);
        }

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

        /// <summary>
        ///A test for isForum
        ///</summary>
        [TestMethod()]
        public void isForumTest() {
            Assert.IsFalse(lg.isForum(-9));
            Assert.IsTrue(lg.isForum(testForumId));
        }

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
    }
}
