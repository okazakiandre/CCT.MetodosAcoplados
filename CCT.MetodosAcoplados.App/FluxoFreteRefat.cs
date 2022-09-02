namespace CCT.MetodosAcoplados.App
{
    public class FluxoFreteRefat : IFluxoFrete
    {
        private IRegiaoRepository RegRepo { get; }

        public FluxoFreteRefat(IRegiaoRepository regRepo)
        {
            RegRepo = regRepo;
        }

        public double CalcularFrete(int numeroCep,
                                    double pesoProduto)
        {
            const double PESO_MINIMO = 2000.0;
            const double FRETE_MINIMO = 5.5;

            var freteRegiao = 0.0;
            if (pesoProduto >= PESO_MINIMO)
            {
                freteRegiao = RegRepo.ObterFreteRegiao(numeroCep);
                if (freteRegiao < FRETE_MINIMO)
                {
                    freteRegiao = FRETE_MINIMO;
                }
            }

            return freteRegiao;
        }
    }
}
