using System.Diagnostics.CodeAnalysis;

namespace Rubin.Static.Models;

public class CategoryTitleComparer : IEqualityComparer<Category>
{
    public bool Equals(Category? x, Category? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x == null || y == null) return false;
        return string.Equals(x.Title.Trim(), y.Title.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode([DisallowNull] Category obj)
    {
        // GetHashCode should be consistent with Equals, use the same comparison logic
        return obj.Title?.ToLowerInvariant().GetHashCode() ?? 0;
    }
}
