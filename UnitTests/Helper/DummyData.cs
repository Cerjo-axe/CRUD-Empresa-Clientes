using DTO;

namespace UnitTests.Helper;

public static class DummyData
{
    public static SegmentoDTO SegmentoInvalido = new SegmentoDTO(){
        Descricao="asdfadasdf"
    };
    public static SegmentoDTO SegmentoValido = new SegmentoDTO(){
        Nome="afsasfas",
        Descricao="adfsdfsadf"
    };
}
