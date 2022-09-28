using ApiMvno.Domain.Attributes;

namespace ApiMvno.Domain.Enums
{
    public enum LineTypeEnum
    {
        [LineType("Normal", 1)]
        Normal,
        [LineType("Gold", 2)]
        Gold,
        [LineType("Did Móvel", 3)]
        DidMovel,
        [LineType("M2M", 4)]
        M2M,
        [LineType("M2M Especial", 5)]
        M2MEspecial = 5,
        [LineType("Teste", 6)]
        Test = 6
    }
}
