using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumSever;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForumServerTest
{
    [TestClass]
    public class ForumServerTest
    {
        //setUP
        int t_count = 2;

        static Database _db = new Database();
        LogicManager _lm = new LogicManager(_db);
        MassageHandler EE = new MassageHandler();

        MemberInfo memb = new MemberInfo("user1", "first1", "last1", "1234", "first1@gmail.com");
        MemberInfo memb2 = new MemberInfo("user2", "first2", "last2", "4321", "first2@gmail.com");
        string massage= "";
        /*
        string return1 = "you have a new friend";
        string return2 = "you have removed yourself as friend";
        string return3 = "your thread was added to the forum";
        string return4 = "your thread was removed from the forum";
        string return5 = "your post was added to the forum";
        string error1 = "user is already a friend with this user";
        string error2 = "the user you are trying to befriend dosn't exist";
        string error3 = "incurrect user name";
        string error4 = "the user you are trying to unfriend is not a friend of yours";
        string error5 = "topic already exists, choose new topic";
        string error6= "the topic could not been found";
        string error7 = "the topic you where trying to remove was submited by a diffrent user";
         */
        int return1 = 1;//friend id
        int return2 = 0;//"you have removed yourself as friend";
        int return3 = 0;// thread id "your thread was added to the forum";
        int return4 = 0;//"your thread was removed from the forum";
        int return5 = 0;// post index"your post was added to the forum";
        int error1 = -1;//"user is already a friend with this user";
        int error2 = -2;//"the user you are trying to befriend dosn't exist";
        int error3 = -3;//"incurrect user name";
        int error4 = -4;//"the user you are trying to unfriend is not a friend of yours";
        int error5 = -5;//"topic already exists, choose new topic";
        int error6 = -6;//"the topic could not been found";
        int error7 = -7;//"the topic you where trying to remove was submited by a diffrent user";
        [TestMethod]
        public void DataBaseTests()
        {
            MemberTests();
            FriendsTests();
            TreadTests();
            PostTest();
        }

        private void MemberTests()
        {
            // register
            Assert.IsTrue(_db.MemberCount() == 0);
            Assert.IsTrue(_lm.register(memb)==0);
            Assert.IsTrue(_lm.register(memb2)==0);
            Assert.IsTrue(_db.MemberCount() == t_count);
            Assert.IsFalse(_lm.register(new MemberInfo("", "first2", "last2", "4321", "first4@gmail.com"))== 0);
            Assert.IsFalse(_lm.register(new MemberInfo("user3", "", "last2", "4321", "first5@gmail.com")) == 0);
            Assert.IsFalse(_lm.register(new MemberInfo("user4", "first2", "", "4321", "first7@gmail.com")) == 0);
            Assert.IsFalse(_lm.register(new MemberInfo("user5", "first2", "last2", "", "first6@gmail.com")) == 0);
            Assert.IsFalse(_lm.register(new MemberInfo("user6", "first2", "last2", "4321", "")) == 0);
            Assert.IsFalse(_lm.register(new MemberInfo("user1", "first2", "last2", "4321", "first9@gmail.com")) == 0);
            Assert.IsFalse(_lm.register(new MemberInfo("user10", "first2", "last2", "4321", "first1@gmail.com")) == 0);
            Assert.IsTrue(_db.MemberCount() == t_count);
            Assert.IsTrue(memb.Equals(_db.FindMemberByUser("user1")));
            Assert.IsFalse(memb.Equals(_db.FindMemberByUser("ssw")));
            Assert.IsTrue(memb.Equals(_db.FindMemberByUser(memb.getUName())));
            Assert.IsTrue(memb2.Equals(_db.FindMemberByUser(memb2.getUName())));
            Assert.IsFalse(_db.FindMemberByUser("user1").getID() == _db.FindMemberByUser("user2").getID());
            Assert.IsTrue(_db.FindMemberByUser("user2").getID() == _db.FindMemberByUser("user2").getID());
            // login-logout
            Assert.IsFalse(_lm.isLogged("user2"));
            Assert.IsTrue(_lm.login("user2", "4321") >= 0);
            Assert.IsFalse(_lm.login("user2", "1234") >= 0);
            Assert.IsFalse(_lm.login("user12", "1234") >= 0);
            Assert.IsTrue(_lm.isLogged("user2"));
            Assert.IsFalse(_lm.login("user12", "1234") >= 0);
            Assert.IsFalse(_lm.logout("user12") == 0);
            Assert.IsTrue(_lm.logout("user12") == -1);
            Assert.IsTrue(_lm.logout("user2") == 0);
            Assert.IsFalse(_lm.logout("user2") == 0);
            Assert.IsFalse(_lm.isLogged("user2"));
        }
        private void FriendsTests()
        {
            //addMeAsFriend
            Assert.AreEqual(_lm.addMeAsFriend(memb2.getID(), memb.getID()), return1);
            Assert.AreEqual(_lm.addMeAsFriend(memb2.getID(), memb2.getID()), return1);
            Assert.AreEqual(_lm.addMeAsFriend(memb2.getID(), memb.getID()), error1);
            Assert.AreEqual(_lm.addMeAsFriend(memb2.getID(), 3), error2);
            Assert.AreEqual(_lm.addMeAsFriend(7, memb2.getID()), error3);
            // removeMeAsFriend
            Assert.AreEqual(_lm.removeMeAsFriend(memb2.getID(), memb.getID()), return2);
            Assert.AreEqual(_lm.removeMeAsFriend(memb2.getID(), memb.getID()), error4);
            Assert.AreEqual(_lm.removeMeAsFriend(memb2.getID(), 3), error2);
            Assert.AreEqual(_lm.removeMeAsFriend(7, 3), error3);

        }

        private void TreadTests()
        {
            // addTread
            Assert.AreEqual(_lm.addTread(memb2.getID(), "topic1", "content2"), return3);
            Assert.AreEqual(_lm.addTread(memb2.getID(), "topic6", "content4"), return3 + 1);
            Assert.AreEqual(_lm.addTread(memb2.getID(), "topic5", "content4"), return3 + 2);
            Assert.AreEqual(_lm.addTread(memb2.getID(), "topic6", "content7"), error5);
            Assert.AreEqual(_lm.addTread(7, "topic6", "content7"), error3);
            Assert.AreEqual(_lm.addTread(memb.getID(), "topic2", "content7"), return3 + 3);
            // getTread
            Assert.AreEqual(_db.getTread("topic6").getID(), _db.getTread("topic1").getID() + 1);
            Assert.AreEqual(_lm.getTread(_db.getTread("topic6").getID()),_db.getTread("topic6"));
            // removeTread
            Assert.AreEqual(_lm.removeTread(memb2.getID(), _db.getTread("topic1").getID()), return4);
            Assert.AreEqual(_lm.removeTread(memb2.getID(), _db.getTread("topic6").getID()), return4);
            Assert.AreEqual(_lm.removeTread(memb2.getID(), 7), error6);
            Assert.AreEqual(_lm.removeTread(7, 7), error3);
            Assert.AreEqual(_lm.removeTread(memb2.getID(), _db.getTread("topic2").getID()), error7);
        }

        private void PostTest()
        {
            // addPost
            Assert.AreEqual(_lm.addPost(memb2.getID(), _db.getTread("topic5").getID(), "post1_topic", "post1_coontent"), return5);
            Assert.AreEqual(_lm.addPost(memb2.getID(), _db.getTread("topic2").getID(), "post1_topic", "post1_coontent"), return5);
            Assert.AreEqual(_lm.addPost(memb.getID(), _db.getTread("topic5").getID(), "post1_topic", "post1_coontent"), return5+1);
            Assert.AreEqual(_lm.addPost(7, _db.getTread("topic5").getID(), "post1_topic", "post1_coontent"), error3);
            Assert.AreEqual(_lm.addPost(memb2.getID(), 7, "post1_topic", "post1_coontent"), error6);
            Assert.AreEqual(_lm.addPost(7, 7, "post1_topic", "post1_coontent"), error3);
            // getPost
           // Assert.AreEqual(_lm.getPost(memb2.getID(), _db.getTread("topic5").getID(), 0), null);
            // removePost
        }

    }
}
