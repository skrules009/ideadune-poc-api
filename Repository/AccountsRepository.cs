using ideadune_pos.Entities;
using ideadune_pos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ideadune_pos.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        readonly dbEntity db;
        public AccountsRepository(dbEntity _db)
        {
            db = _db;
        }



        public async Task<List<BOAccounts>> GetAccounts()
        {
            if (db != null)
            {
                return await (from p in db.accountsInfomation
                              select new BOAccounts
                              {
                                  accountId = p.accountId,
                                  name = p.name,
                                  type = p.type,
                                  address = p.address,
                                  doorCode = p.doorCode,
                                  phone = p.phone,
                                  website = p.website,
                                  facebook = p.facebook,
                                  size = p.size,
                                  additionalInfo = p.additionalInfo,
                                  office = p.office,
                                  crr = p.crr,
                                  zone = p.zone,
                                  visitFrequency = p.visitFrequency,
                                  contactStatus = p.contactStatus,
                                  infoForClinicalTeam = p.infoForClinicalTeam,
                              }).ToListAsync();
            }
            return null;
        }
        public async Task<bool> AddNote(NoteWithMontion notewithmention)
        {
            if (db != null)
            {
                BONote boNote = notewithmention.BONote;
               
                if (boNote.noteId > 0) // Update the notes 
                {
                    var dbentry = db.notes.FirstOrDefault(x => x.noteId == boNote.noteId);
                    dbentry.notes = boNote.notes;
                    dbentry.updatedDt = dbentry.pin == boNote.pin? dbentry.updatedDt : System.DateTime.Now;
                    dbentry.pin = boNote.pin;
                    dbentry.timeIn = boNote.timeIn;
                    dbentry.timeOut = boNote.timeOut;
                    dbentry.typeOfContact = boNote.typeOfContact;
                    await db.SaveChangesAsync();
                    //// To Remove the Mention entry from the table
                    //var mention = db.mentionInfo.Where(d => d.noteId == boNote.noteId).ToList();
                    //db.mentionInfo.RemoveRange(mention);
                    //await db.SaveChangesAsync();
                    // Insert new record in the table
                    await SaveMention(notewithmention,true);
                    //foreach (var item in notewithmention?.mention)
                    //{
                    //    item.choice.noteId = boNote.noteId;
                    //    item.choice.id = 0;
                    //    item.choice.start = item.indices.start;
                    //    item.choice.end = item.indices.end;
                    //    await db.mentionInfo.AddAsync(item.choice);
                    //    await db.SaveChangesAsync();
                    //}
                    return true;
                }
                boNote.createdDt = System.DateTime.Now;
                boNote.updatedDt = System.DateTime.Now;
                await db.notes.AddAsync(boNote);
                await db.SaveChangesAsync();
                await SaveMention(notewithmention,false);
                //int id= db.notes.Select(x => x.noteId).Max();
                //foreach (var item in notewithmention?.mention)
                //{
                //    item.choice.noteId = id;
                //    item.choice.id = 0;
                //    item.choice.start = item.indices.start;
                //    item.choice.end = item.indices.end;
                //    await db.mentionInfo.AddAsync(item.choice);
                //    await db.SaveChangesAsync();
                //}
                //return true;
            }
            return false;
        }
        public async Task<bool> SaveMention(NoteWithMontion notewithmention,bool isUpdate)
        {
            int id = isUpdate? notewithmention.BONote.noteId : db.notes.Select(x => x.noteId).Max();
            if (isUpdate)
            {
                var mention = db.mentionInfo.Where(d => d.noteId == id).ToList();
                db.mentionInfo.RemoveRange(mention);
                await db.SaveChangesAsync();
            }
            foreach (var item in notewithmention?.mention)
            {
                item.choice.noteId = id;
                item.choice.id = 0;
                item.choice.start = item.indices.start;
                item.choice.end = item.indices.end;
                await db.mentionInfo.AddAsync(item.choice);
                await db.SaveChangesAsync();
            }
            return true;
        }
        public async Task<List<BONote>> GetNotes(int accountId)
        {
            if (db != null)
            {
               var notesData= (from p in db.notes
                              join
           k in db.login on p.createdBy equals k.userId
                              where p.accountId == accountId
                              orderby p.pin descending, p.updatedDt descending
                              select new BONote
                              {
                                  accountId = p.accountId,
                                  notes = p.notes,
                                  createdDt = p.createdDt,
                                  typeOfContact = p.typeOfContact,
                                  pin = p.pin,
                                  timeIn = p.timeIn,
                                  timeOut = p.timeOut,
                                  noteId = p.noteId,
                                  createdBy = k.firstName + ' ' + k.lastName,
                                  commentList = (from c in db.commentInfo
                                                 join l in db.login on c.createdBy equals l.userId
                                                 where c.noteId == p.noteId
                                                 select new BOCommentList
                                                 {
                                                     noteId = c.noteId,
                                                     comment = c.comment,
                                                     accountId = c.accountId,
                                                     commentId = c.commentId,
                                                     createdDt = c.createdDt,
                                                     createdBy = l.firstName + ' ' + l.lastName
                                                 }).ToList() ?? new List<BOCommentList>(),
                                  choiceList = (from m in db.mentionInfo
                                                where m.noteId == p.noteId
                                                select new ChoiceList
                                                {
                                                    noteId = m.noteId,
                                                    id = m.id,
                                                    name =Convert.ToString(m.name),
                                                    userId = Convert.ToString(m.userId),
                                                    start = Convert.ToInt32(m.start),
                                                    end = Convert.ToInt32(m.end)
                                                }).ToList() ?? new List<ChoiceList>()

                              }).ToListAsync();
                return await notesData;
            }
            return null;
        }

        public async Task<bool> AddComment(BOComment comment)
        {
            if (db != null)
            {
                comment.createdDt = System.DateTime.Now;
                await db.commentInfo.AddAsync(comment);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
