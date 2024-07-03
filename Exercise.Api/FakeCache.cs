namespace Exercise.Api;

public class FakeCache : List<Domain>
{
    public async Task<Domain> SingleAsync(Func<Domain, bool> predicate)
    {
        return await Task.FromResult(this.Single(predicate));
    }

    public async Task AddAsync(Domain domain)
    {
        Add(domain);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Domain domain)
    {
        var find = await SingleAsync(x => x.Uuid == domain.Uuid);
        find.Id = domain.Id;
    }

    public async Task DeleteAsync(Domain domain)
    {
        var find = await SingleAsync(x => x.Uuid == domain.Uuid);
        Remove(find);
    }

    public async Task<List<Domain>> ToListAsync()
    {
        return await Task.FromResult(this.ToList());
    }
}