namespace MvcExampleP421.Models;

public class ImageFile
{
    public int Id { get; set; }
    public string FileName { get; set; }

    public string Src
    {
        get
        {
            // Повернення шляху до файлу для використання в HTML
            return $"/uploads/images/{FileName[0]}/{FileName[1]}/{FileName}";
        }
    }
}
