using System.Security.Policy;

namespace MvcExampleP421.Models;

public class ImageFileStorageService(IWebHostEnvironment environment)
{
    /// <summary>
    /// Зберігає файл на диск і повертає унікальне ім'я файлу для збереження в базі даних.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<string> StoreFileAsync(IFormFile file)
    {
        // Перевірка вхідних даних
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is null or empty", nameof(file));
        }

        // Генерація унікального імені файлу
        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        
        // Отримання шляху для збереження файлу
        var filePath = GetFilePath(uniqueFileName);

        // Створення директорії, якщо вона не існує
        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        }

        // Збереження файлу на диск
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        // Повернення унікального імені файлу для збереження в базі даних
        return uniqueFileName;
    }

    /// <summary>
    /// Генерує шлях до файлу на основі унікального імені файлу.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private string GetFilePath(string fileName)
    {
        // Перевірка вхідних даних
        if (string.IsNullOrEmpty(fileName) || fileName.Length < 2)
        {
            throw new ArgumentException("Invalid file name", nameof(fileName));
        }

        // Формування шляху до файлу
        var uploadsRootFolder = Path.Combine(environment.WebRootPath, "uploads", "images");

        // UUID : f1e2d3c4-b5a6-7d8e-9f0a-b1c2d3e4f5a6
        // 16 директорій 1 рівень
        // 16 директорій 2 рівень

        // file.txt         wwwroot/uploads/images/f/i/file.txt

        // Використовуємо перші два символи імені файлу для створення підпапок
        var subDir1 = fileName.Substring(0, 1);
        var subDir2 = fileName.Substring(1, 1);
        var filePath = Path.Combine(uploadsRootFolder, subDir1, subDir2, fileName);
        return filePath;
    }

    public void DeleteFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName) || fileName.Length < 2)
        {
            throw new ArgumentException("Invalid file name", nameof(fileName));
        }
        var filePath = GetFilePath(fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
