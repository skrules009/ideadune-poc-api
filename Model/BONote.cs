using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ideadune_pos.Model
{
    public partial class NoteWithMontion
    {
        public BONote BONote { get; set; }
        public List<BOMention> mention { get; set; }
    }
    // [{"choice":{"name":"Niket gupta","id":4,"userId":"niket"},"indices":{"start":11,"end":23}},{"choice":{"name":"santosh shukla","id":2,"userId":"aks"},"indices":{"start":50,"end":65}},{"choice":{"name":"HARRY BANN","id":3,"userId":"HARRY"},"indices":{"start":107,"end":118}}]
    public partial class BOMention
    {
        public Choice choice { get; set; }
        public Indices indices { get; set; }
    }
    public partial class Choice
    {
        public string name { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string userId { get; set; }
        public int noteId { get; set; }
        public int start { get; set; }
        public int end { get; set; }
    }
    public partial class ChoiceList
    {
        public string name { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string userId { get; set; }
        public int noteId { get; set; }
        public int start { get; set; }
        public int end { get; set; }
    }
    public partial class Indices
    {
        public int start { get; set; }
        public int end { get; set; }
    }
    public partial class BONote
    {
        public int accountId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int noteId { get; set; }
        public string notes { get; set; }
        public DateTime? createdDt { get; set; }
        public string typeOfContact { get; set; }
        public bool pin { get; set; }
        public string timeIn { get; set; }
        public string timeOut { get; set; }
        public string createdBy { get; set; }
        public DateTime? updatedDt { get; set; }
        public List<BOCommentList> commentList { get; set; }
        public List<ChoiceList> choiceList { get; set; }
    }
    public partial class BOCommentList
    {
        public int accountId { get; set; }
        public int noteId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int commentId { get; set; }
        public string comment { get; set; }
        public DateTime? createdDt { get; set; }
        public string createdBy { get; set; }
    }
    public partial class BOComment
    {
        public int accountId { get; set; }
        public int noteId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int commentId { get; set; }
        public string comment { get; set; }
        public DateTime? createdDt { get; set; }
        public string createdBy { get; set; }
    }
}
