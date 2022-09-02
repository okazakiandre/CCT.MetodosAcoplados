namespace CCT.MetodosAcoplados.App
{
    public class FluxoPedido
    {
        public Pedido CalcularPedido(List<ProdutoPedido> produtos,
                                     int numeroCep)
        {
            var ped = new Pedido();

            foreach (var prodPed in produtos)
            {
                var prodRepo = new ProdutoRepository();
                var prod = prodRepo.ObterProduto(prodPed.IdProduto);

                var valorFrete = 0.0;
                if (!prod.TemPromocaoFreteGratis)
                {
                    var flxFrete = new FluxoFrete();
                    valorFrete = flxFrete.CalcularFrete(numeroCep,
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
