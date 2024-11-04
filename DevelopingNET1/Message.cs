using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace DevelopingNET1
{
    internal class Message
    {
        public string FromName { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public  string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public static Message? FromJson(string somemessage)
        {
            return JsonSerializer.Deserialize<Message>(somemessage);
        }
        public Message(string nikname, string text) {
            this.FromName = nikname;
            this.Text = text;
            this.Date = DateTime.Now;
        }
        public Message() { }
        public override string ToString()
        {
            return $"Получено сообщение от {FromName} ({Date.ToShortTimeString()}): \n {Text}";
        }
    }
}
