using CCT.MetodosAcoplados.App;
using Moq;

namespace CCT.MetodosAcoplados.UnitTest
{
    [TestClass]
    [TestCategory("UnitTest > FluxoPedidoRefat")]
    public class FluxoPedidoRefatTest
    {
        [TestMethod]
        public void NaoDeveriaCalcularPedidoSemProdutos()
        {
            var lista = new List<ProdutoPedido>();
            var cep = 0;
            var mockRepo = new Mock<IProdutoRepository>();
            var mockFrete = new Mock<IFluxoFrete>();
            var fluxo = new FluxoPedidoRefat(mockRepo.Object, mockFrete.Object);

            var res = fluxo.CalcularPedido(lista, cep);

            Assert.AreEqual(0, res.ValorTotal);
            Assert.AreEqual(0, res.ValorFrete);
        }

        [TestMethod]
        public void DeveriaCalcularPedidoComFrete()
        {
            var lista = new List<ProdutoPedido>
            {
                new ProdutoPedido { IdProduto = 1, Quantidade = 2 }
            };
            var cep = 12345001;
            var prod = new Produto(1, 100, 10.0, false);
            var mockRepo = new Mock<IProdutoRepository>();
            mockRepo.Setup(m => m.ObterProduto(It.IsAny<int>())).Returns(prod);
            var mockFrete = new Mock<IFluxoFrete>();
            mockFrete.Setup(m => m.CalcularFrete(It.IsAny<int>(), It.IsAny<double>())).Returns(8.1);
            var fluxo = new FluxoPedidoRefat(mockRepo.Object, mockFrete.Object);

            var res = fluxo.CalcularPedido(lista, cep);

            Assert.AreEqual(28.1, res.ValorTotal);
            Assert.AreEqual(8.1, res.ValorFrete);
        }

        [TestMethod]
        public void DeveriaCalcularPedidoSemFreteParaProdutoNaPromocao()
        {
            var lista = new List<ProdutoPedido>
            {
                new ProdutoPedido { IdProduto = 1, Quantidade = 2 }
            };
            var cep = 12345001;
            var prod = new Produto(1, 100, 10.0, true);
            var mockRepo = new Mock<IProdutoRepository>();
            mockRepo.Setup(m => m.ObterProduto(It.IsAny<int>())).Returns(prod);
            var mockFrete = new Mock<IFluxoFrete>();
            var fluxo = new FluxoPedidoRefat(mockRepo.Object, mockFrete.Object);

            var res = fluxo.CalcularPedido(lista, cep);

            Assert.AreEqual(20.0, res.ValorTotal);
            Assert.AreEqual(0, res.ValorFrete);
        }
    }
}