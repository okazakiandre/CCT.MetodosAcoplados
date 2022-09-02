namespace CCT.MetodosAcoplados.App
{
    public class Pedido
    {
        public List<ProdutoPedido> Produtos { get; set; }
        public double ValorFrete { get; set; }
        public double ValorTotal { get; set; }

        public Pedido()
        {
            Produtos = new List<ProdutoPedido>();
        }
    }
}
