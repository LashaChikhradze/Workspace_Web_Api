using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Workspace_Models.Enums;

namespace Workspace_Models
{
    public class Assignment : BaseEntity
    {
        public int? SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        public int? RecieverId { get; set; }
        [ForeignKey("RecieverId")]
        public User Reciever { get; set; }
        public string Text { get; set; }
        public TaskStatus Status { get; set; }
        public TaskCancel Cancel { get; set; }
    }
}
