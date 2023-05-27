using Briedge;

Sender sender = new EmailSender(new DataBaseReader());
sender.Send();

sender.SetDatareader(new FronFileReader());
sender.Send();

sender = new TelegramSender(new DataBaseReader());
sender.Send();