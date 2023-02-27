using Microsoft.EntityFrameworkCore;
using Bookstore.Models;

namespace Bookstore.DAL.Repositories;


public class LanguageRepository : ILanguageRepository
{
    DatabaseApplicationContext _db;

    public LanguageRepository(DatabaseApplicationContext db)
    {
        _db = db;
    }

    public async void add(Language language)
    {
        Language? tempLanguage = await getById(language.Id);
        if (tempLanguage == null)
        {
            _db.Languages.Add(language);
            _db.SaveChanges();
        }
    }

    public async Task<Language> getById(int id)
    {
        Language? language = await _db.Languages
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        return language;
    }

    public async Task<Language> getByIdIncluded(int id)
    {
        Language? language = await _db.Languages
            .Where(u => u.Id == id)
            .Include(u => u.BookInfos)
            .ThenInclude(u => u.Book)
            .FirstOrDefaultAsync();

        return language;
    }

    public async Task<List<Language>> getAll()
    {
        List<Language> languages = await _db.Languages
            .OrderBy(u => u.Name)
            .ToListAsync();

        return languages;
    }

    public async Task<List<Language>> getAllIncluded()
    {
        List<Language> languages = await _db.Languages
            .Include(u => u.BookInfos)
            .ThenInclude(u => u.Book)
            .OrderBy(u => u.Name)
            .ToListAsync();

        return languages;
    }

    public async void update(Language language)
    {
        Language? tempLanguage = await getById(language.Id);
        if (tempLanguage == null)
            _db.Languages.Add(language);
        else
            tempLanguage.Name = language.Name;

        _db.SaveChanges();
    }

    public async void delete(int id)
    {
        Language? language = await getById(id);
        if (language != null)
        {
            _db.Languages.Remove(language);
            _db.SaveChanges();
        }
    }
}