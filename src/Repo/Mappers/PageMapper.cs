using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure.DTO;


namespace GBReaderCremaL.Repo.Mappers;

public class PageMapper
{
    public static List<Page> ToPages(List<DTOPage> dtoPages)
    {
        List<Page> pages = new List<Page>();

        foreach (DTOPage dtoP in dtoPages)
        {
            pages.Add(new Page(dtoP.NumSrc, dtoP.Txt, PathMapper.ToPaths(dtoP.Dst)));
        }
        
        
        return pages;
    }
}