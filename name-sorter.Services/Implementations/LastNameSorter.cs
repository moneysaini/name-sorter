using name_sorter.Services.Interfaces;

namespace name_sorter.Services.Implementations
{
    public class LastNameSorter : INameSorter
    {
        public List<string> SortNames(List<string> names)
        {
            return names.OrderBy(name => name.Split().Last())
                        .ThenBy(name => name)
                        .ToList();
        }
    }
}
