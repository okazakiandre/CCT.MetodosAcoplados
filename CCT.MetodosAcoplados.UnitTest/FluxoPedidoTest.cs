using CCT.MetodosAcoplados.App;

namespace CCT.MetodosAcoplados.UnitTest
{
    [TestClass]
    [TestCategory("UnitTest > FluxoPedido")]
    public class FluxoPedidoTest
    {
        [TestMethod]
        public void NaoDeveriaCalcularPedidoSemProdutos()
        {
            var lista = new List<ProdutoPedido>();
            var cep = 0;
            var fluxo = new FluxoPedido();

            var res = fluxo.CalcularPedido(lista, cep);

            Assert.AreEqual(0, res.ValorTotal);
            Assert.AreEqual(0, res.ValorFrete);
        }

        [TestMethod]
        public void DeveriaCalcularPedidoComFrete()
        {
            var lista = new List<ProdutoPedido>
            {
                new ProdutoPedido { IdProduto = 1, Quantidade = 2 },
                new ProdutoPedido { IdProduto = 3, Quantidade = 1 }
            };
            var cep = 12345001;
            var fluxo = new FluxoPedido();

            var res = fluxo.CalcularPedido(lista, cep);

            Assert.AreEqual(55.0, res.ValorTotal);
            Assert.AreEqual(15.0, res.ValorFrete);
        }

        [TestMethod]
        public void DeveriaCalcularPedidoSemFreteParaProdutoNaPromocao()
        {
            var lista = new List<ProdutoPedido>
            {
                new ProdutoPedido { IdProduto = 1, Quantidade = 2 },
                new ProdutoPedido { IdProduto = 4, Quantidade = 4 }
            };
            var cep = 12345001;
            var fluxo = new FluxoPedido();

            var res = fluxo.CalcularPedido(lista, cep);

            Assert.AreEqual(68.8, res.ValorTotal);
            Assert.AreEqual(0.0, res.ValorFrete);
        }


        [TestMethod]
        public void DeveriaCalcularPedidoSemFreteParaProdutoAbaixoDoPesoMinimo()
        {
            var lista = new List<ProdutoPedido>
            {
                new ProdutoPedido { IdProduto = 3, Quantidade = 2 }
            };
            var cep = 12345001;
            var fluxo = new FluxoPedido();

            var res = fluxo.CalcularPedido(lista, cep);

            Assert.AreEqual(70.0, res.ValorTotal);
            Assert.AreEqual(0.0, res.ValorFrete);
        }

        [TestMethod]
        public void DeveriaCalcularPedidoComFreteMinimo()
        {
            var lista = new List<ProdutoPedido>
            {
                new ProdutoPedido { IdProduto = 2, Quantidade = 2 }
            };
            var cep = 1000;
            var fluxo = new FluxoPedido();

            var res = fluxo.CalcularPedido(lista, cep);

            Assert.AreEqual(45.5, res.ValorTotal);
            Assert.AreEqual(5.5, res.ValorFrete);
        }
    }
}