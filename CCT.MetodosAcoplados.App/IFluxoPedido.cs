namespace CCT.MetodosAcoplados.App
{
    public interface IFluxoPedido
    {
        Pedido CalcularPedido(List<ProdutoPedido> produtos,
                              int numeroCep);
    }
}
