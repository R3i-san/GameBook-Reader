using GBReaderCremaL.Infrastructure.DTO;
using Path = GBReaderCremaL.Domains.Path;


namespace GBReaderCremaL.Repo.Mappers;

public class PathMapper
{
    public static List<Path> ToPaths(List<DTOPath> dtoPaths)
    {

        List<Path> paths = new List<Path>();
        foreach (DTOPath dtoPath in dtoPaths)
        {
            paths.Add(new Path(dtoPath.Dst, dtoPath.Txt));
        }
        return paths;
    }
}