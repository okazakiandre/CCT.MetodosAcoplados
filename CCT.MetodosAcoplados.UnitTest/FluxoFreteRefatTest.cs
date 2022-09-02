using CCT.MetodosAcoplados.App;
using Moq;

namespace CCT.MetodosAcoplados.UnitTest
{
    [TestClass]
    [TestCategory("UnitTest > FluxoFreteRefat")]
    public class FluxoFreteRefatTest
    {
        [TestMethod]
        public void NaoDeveriaCalcularFreteComPesoAbaixoDoMinimo()
        {
            var cep = 10000;
            var peso = 1999.0;
            var mockRepo = new Mock<IRegiaoRepository>();
            var fluxo = new FluxoFreteRefat(mockRepo.Object);

            var res = fluxo.CalcularFrete(cep, peso);

            Assert.AreEqual(0, res);
        }

        [TestMethod]
        public void DeveriaCalcularFreteComOValorMinimo()
        {
            var cep = 10000;
            var peso = 2000.0;
            var mockRepo = new Mock<IRegiaoRepository>();
            mockRepo.Setup(m => m.ObterFreteRegiao(It.IsAny<int>())).Returns(5.4);
            var fluxo = new FluxoFreteRefat(mockRepo.Object);

            var res = fluxo.CalcularFrete(cep, peso);

            Assert.AreEqual(5.5, res);
        }

        [TestMethod]
        public void DeveriaCalcularFreteComOValorCadastrado()
        {
            var cep = 10000;
            var peso = 2000.0;
            var mockRepo = new Mock<IRegiaoRepository>();
            mockRepo.Setup(m => m.ObterFreteRegiao(It.IsAny<int>())).Returns(11.1);
            var fluxo = new FluxoFreteRefat(mockRepo.Object);

            var res = fluxo.CalcularFrete(cep, peso);

            Assert.AreEqual(11.1, res);
        }
    }
}