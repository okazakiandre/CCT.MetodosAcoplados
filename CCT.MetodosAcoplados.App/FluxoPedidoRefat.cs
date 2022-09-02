namespace CCT.MetodosAcoplados.App
{
    public class FluxoPedidoRefat : IFluxoPedido
    {
        private IProdutoRepository ProdRepo { get; }
        private IFluxoFrete FlxFrete { get; }

        public FluxoPedidoRefat(IProdutoRepository prodRepo, 
                                IFluxoFrete flxFrete)
        {
            ProdRepo = prodRepo;
            FlxFrete = flxFrete;
        }

        public Pedido CalcularPedido(List<ProdutoPedido> produtos,
                                     int numeroCep)
        {
            var ped = new Pedido();

            foreach (var prodPed in produtos)
            {
                var prod = ProdRepo.ObterProduto(prodPed.IdProduto);

                var valorFrete = 0.0;
                if (!prod.TemPromocaoFreteGratis)
                {
                    valorFrete = FlxFrete.CalcularFrete(numeroCep,
                                                        prod.Peso);
                }

                ped.Produtos.Add(prodPed);
                ped.ValorTotal += prod.Preco * prodPed.Quantidade;
                ped.ValorFrete += valorFrete;
            }

            ped.ValorTotal += ped.ValorFrete;
            return ped;
        }
    }
}
