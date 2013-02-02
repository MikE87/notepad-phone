using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NotepadServiceRole
{
    [ServiceContract]
    public interface INotepad
    {

        [OperationContract]
        UserDC CheckUser(string userName, string userPass);

        [OperationContract]
        UserDC AddUser(string userName, string userPass, string userDescription);

        [OperationContract]
        void UpdateUser(int userID, string userDescription);

        [OperationContract]
        void DeleteUser(int userID);

        [OperationContract]
        NoteDC AddNote(int userID, string noteDescription, string noteText);

        [OperationContract]
        void UpdateNote(int noteID, string noteText);

        [OperationContract]
        void DeleteNote(int noteID);

        [OperationContract]
        void AddGroup(string groupName, string groupDescription);

        [OperationContract]
        void UpdateGroup(int groupID, string groupDescription);

        [OperationContract]
        void DeleteGroup(int groupID);

        [OperationContract]
        void AddUserToGroup(int userID, int groupID);

        [OperationContract]
        void AddNoteToGroup(int noteID, int groupID);

        [OperationContract]
        void RemoveUserFromGroup(int userID, int groupID);

        [OperationContract]
        void RemoveNoteFromGroup(int noteID, int groupID);

        [OperationContract]
        UserDC GetUser(int userID);

        [OperationContract]
        NoteDC GetNote(int noteID);

        [OperationContract]
        GroupDC GetGroup(int groupID);

        [OperationContract]
        List<NoteDC> GetUserNotes(int userID);

        [OperationContract]
        List<NoteDC> GetGroupNotes(int groupID);

        [OperationContract]
        List<UserDC> GetGroupUsers(int groupID);
    }


    [DataContract]
    public class NoteDC
    {
        [DataMember]
        public int NoteId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string NoteText { get; set; }

        [DataMember]
        public int UserId { get; set; }
    }

    [DataContract]
    public class UserDC
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Pass { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class GroupDC
    {
        [DataMember]
        public int GroupId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
