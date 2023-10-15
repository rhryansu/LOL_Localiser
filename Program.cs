using System.Diagnostics;

Console.WriteLine("Start processing localisation...");

string source = @".\asset\league_of_legends.live.product_settings.yaml";
string destination = @"C:\ProgramData\Riot Games\Metadata\league_of_legends.live\league_of_legends.live.product_settings.yaml";
string? lineToWrite = null;

if (!File.Exists(source))
{
    Process.Start(new ProcessStartInfo(@$"C:\Users\{Environment.UserName}\Desktop\LeagueClient.exe.lnk") { UseShellExecute = true });
    Console.WriteLine("Source file not found. Initialising...");
    Thread.Sleep(5000);

    using (StreamReader reader = new(destination))
    {
        for (int i = 1; i <= 18; ++i)
            lineToWrite = reader.ReadLine();
        if (lineToWrite != "    locale: \"zh_CN\"")
        {
            lineToWrite = "    locale: \"zh_CN\"";
        }
    }

    int line_num = 1;
    string? line = null;

    Directory.CreateDirectory(@".\asset");

    using (StreamReader reader = new(destination))
    using (StreamWriter writer = File.AppendText(source))
    {
        while ((line = reader.ReadLine()) != null)
        {
            if (line_num == 18)
            {
                writer.WriteLine(lineToWrite);
            }
            else
            {
                writer.WriteLine(line);
            }
            line_num++;
        }
    }
}

File.Delete(destination);
File.Copy(source, destination);
Console.WriteLine("File has been REPLACED at destination.");
Thread.Sleep(5000);
Process.Start(new ProcessStartInfo(@$"C:\Users\{Environment.UserName}\Desktop\LeagueClient.exe.lnk") { UseShellExecute = true });