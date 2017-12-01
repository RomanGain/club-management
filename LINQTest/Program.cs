using System;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Linq;

namespace ClubInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString;
            SqlConnection connection;

            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                connection = new SqlConnection(connectionString);
                connection.Open();
                DataContext db = new DataContext(connectionString);

                VisitorsEntry(db);
                SongsEntry(db);
                OutputClub(db);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //connection.Close();
                Console.WriteLine("Подключение закрыто...");
                Console.ReadLine();
            }
        }

        static void VisitorsEntry(DataContext db)
        {
            try
            {
                ClubVisitor cv;
                Console.WriteLine("Укажите количество посетителей в клубе:");
                int visitorsCounter = Convert.ToInt32(Console.ReadLine());

                for (int i = 1; i <= visitorsCounter; i++)
                {
                    Console.WriteLine("Введите имя {0}-го посетителя:", i);
                    Console.WriteLine("Укажите стиль его/её танцев:");
                    Console.WriteLine("(Через enter)");


                    cv = new ClubVisitor { personName = Console.ReadLine(), personSkill = Console.ReadLine() };
                    db.GetTable<ClubVisitor>().InsertOnSubmit(cv);

                    Console.Clear();
                }
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void SongsEntry(DataContext db)
        {
            try
            {
                ClubSong cs;
                Console.WriteLine("Укажите количество песен в клубе:");
                int songsCounter = Convert.ToInt32(Console.ReadLine());

                for (int i = 1; i <= songsCounter; i++)
                {
                    Console.WriteLine("Введите имя {0}-й песни:", i);
                    Console.WriteLine("Укажите стиль:");
                    Console.WriteLine("(Через enter)");


                    cs = new ClubSong { songTitle = Console.ReadLine(), songStyle = Console.ReadLine() };
                    db.GetTable<ClubSong>().InsertOnSubmit(cs);

                    Console.Clear();
                }
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void OutputClub(DataContext db)
        {
            try
            {
                string song_style;
                foreach (var song in db.GetTable<ClubSong>())
                {
                    Console.WriteLine("Играет песня - {0}. Жанр - {1}.", song.songTitle, song.songStyle);
                    song_style = song.songStyle;

                    Console.WriteLine("Персонажи танцуют:");
                    var query = from u in db.GetTable<ClubVisitor>()
                                where u.personSkill == song_style
                                select u;

                    foreach (var visitorsOnTheDancefloor in query)
                    {
                        Console.WriteLine(visitorsOnTheDancefloor.personName);
                    }


                    Console.WriteLine("Персонажи в баре:");
                    query = from u in db.GetTable<ClubVisitor>()
                            where u.personSkill != song_style
                            select u;

                    foreach (var visitorsAtTheBar in query)
                    {
                        Console.WriteLine(visitorsAtTheBar.personName);
                    }
                    Console.WriteLine("-----------------------------------------");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
