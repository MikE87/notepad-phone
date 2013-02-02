using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;

namespace NotepadServiceRole
{
    public class Service1 : INotepad
    {

        UserDC INotepad.CheckUser(string userName, string userPass)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var user = context.User.Where(u => u.Name == userName).SingleOrDefault();

                    if (user == null)
                    {
                        return null;
                    }
                    else if (user.Password.Equals(userPass))
                    {
                        return new UserDC
                        {
                            UserId = user.Id,
                            Name = user.Name,
                            Pass = user.Password,
                            Description = user.Description
                        };
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        UserDC INotepad.AddUser(string userName, string userPass, string userDescription)
        {
            try
            {
                User user = new User
                {
                    Name = userName,
                    Password = userPass,
                    Description = userDescription
                };

                using(var context = new NotepadSharedEntities())
                {
                    context.User.Add(user);
                    context.SaveChanges();

                    return new UserDC
                    {
                        UserId = user.Id,
                        Pass = user.Password,
                        Name = user.Name,
                        Description = user.Description
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        void INotepad.UpdateUser(int userID, string userDescription)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var user = context.User.Where(u => u.Id == userID).Single();

                    user.Description = userDescription;
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.DeleteUser(int userID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var user = context.User.Where(u => u.Id == userID).Single();

                    context.User.Remove(user);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        NoteDC INotepad.AddNote(int userID, string noteDescription, string noteText)
        {
            try
            {
                Note note = new Note
                {
                    Description = noteDescription,
                    Content = noteText,
                    User_Id = userID
                };

                using (var context = new NotepadSharedEntities())
                {
                    context.Note.Add(note);
                    context.SaveChanges();
                }
                return new NoteDC
                {
                    NoteId = note.Id,
                    Description = note.Description,
                    NoteText = note.Content,
                    UserId = note.User_Id
                };
            }
            catch
            {
                return null;
            }
        }

        void INotepad.UpdateNote(int noteID, string noteText)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var note = context.Note.Where(n => n.Id == noteID).Single();

                    note.Content = noteText;
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.DeleteNote(int noteID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var note = context.Note.Where(n => n.Id == noteID).Single();

                    context.Note.Remove(note);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.AddGroup(string groupName, string groupDescription)
        {
            try
            {
                Group group = new Group
                {
                    Name = groupName,
                    Description = groupDescription
                };

                using (var context = new NotepadSharedEntities())
                {
                    context.Group.Add(group);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.UpdateGroup(int groupID, string groupDescription)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var gr = context.Group.Where(g => g.Id == groupID).Single();

                    gr.Description = groupDescription;
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.DeleteGroup(int groupID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var gr = context.Group.Where(g => g.Id == groupID).Single();

                    context.Group.Remove(gr);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.AddUserToGroup(int userID, int groupID)
        {
            try
            {
                GroupUser guer = new GroupUser
                {
                    User_Id = userID,
                    Group_Id = groupID
                };

                using (var context = new NotepadSharedEntities())
                {
                    context.GroupUser.Add(guer);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.AddNoteToGroup(int noteID, int groupID)
        {
            try
            {
                GroupNote gnot = new GroupNote
                {
                    Note_Id = noteID,
                    Group_Id = groupID
                };

                using (var context = new NotepadSharedEntities())
                {
                    context.GroupNote.Add(gnot);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.RemoveUserFromGroup(int userID, int groupID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var gu = context.GroupUser.Where(g => g.User_Id == userID 
                                                       && g.Group_Id == groupID).Single();

                    context.GroupUser.Remove(gu);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        void INotepad.RemoveNoteFromGroup(int noteID, int groupID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var gn = context.GroupNote.Where(g => g.Note_Id == noteID 
                                                       && g.Group_Id == groupID).Single();

                    context.GroupNote.Remove(gn);
                    context.SaveChanges();
                }
            }
            catch { }
        }

        UserDC INotepad.GetUser(int userID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var user = context.User.Where(u => u.Id == userID).Single();

                    return new UserDC
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Description = user.Description
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        NoteDC INotepad.GetNote(int noteID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var note = context.Note.Where(n => n.Id == noteID).Single();

                    return new NoteDC
                    {
                        NoteId = note.Id,
                        Description = note.Description,
                        NoteText = note.Content
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        GroupDC INotepad.GetGroup(int groupID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var group = context.Group.Where(g => g.Id == groupID).Single();

                    return new GroupDC
                    {
                        GroupId = group.Id,
                        Name = group.Name,
                        Description = group.Description
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        List<NoteDC> INotepad.GetUserNotes(int userID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var notes = (from n in context.Note
                                 where n.User_Id == userID
                                 orderby n.Description ascending
                                 select new NoteDC
                                 {
                                     NoteId = n.Id,
                                     Description = n.Description,
                                     NoteText = n.Content
                                 }).ToList<NoteDC>();

                    return notes;
                }
            }
            catch
            {
                return null;
            }
        }

        List<NoteDC> INotepad.GetGroupNotes(int groupID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var gr = context.GroupNote.Where(g => g.Group_Id == groupID).Select(g => g.Note_Id);

                    var notes = (from n in context.Note
                                 where gr.Contains(n.Id)
                                 orderby n.Description ascending
                                 select new NoteDC
                                 {
                                     NoteId = n.Id,
                                     Description = n.Description,
                                     NoteText = n.Content
                                 }).ToList<NoteDC>();

                    return notes;
                }
            }
            catch
            {
                return null;
            }
        }

        List<UserDC> INotepad.GetGroupUsers(int groupID)
        {
            try
            {
                using (var context = new NotepadSharedEntities())
                {
                    var gr = context.GroupUser.Where(g => g.Group_Id == groupID).Select(g => g.User_Id);

                    var users = (from u in context.User
                                 where gr.Contains(u.Id)
                                 orderby u.Description ascending
                                 select new UserDC
                                 {
                                     UserId = u.Id,
                                     Name = u.Name,
                                     Description = u.Description
                                 }).ToList<UserDC>();

                    return users;
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
