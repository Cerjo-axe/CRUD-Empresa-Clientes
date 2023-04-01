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

    public static ClienteDTO ClienteInvalido = new ClienteDTO(){
        Nome="asdfasd"
    };
    public static ClienteDTO ClienteValido = new ClienteDTO(){
        Nome="asdfasd",
        Site="safsdfsdaf",
        SegmentoId=new Guid().ToString()
    };
}
