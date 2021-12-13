using APICatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            { Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa1"), new Jogo{ Id = Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa1"), Nome = "Dragons Dogma", Produtora = "Capcom", Preco = 100}},
            { Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa2"), new Jogo{ Id = Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa2"), Nome = "Dragon Age", Produtora = " BioWare", Preco = 200}},
            { Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa3"), new Jogo{ Id = Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa3"), Nome = "Street Fighter V", Produtora = " Capcom", Preco = 80}},
            { Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa4"), new Jogo{ Id = Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa4"), Nome = "Grand Theft Auto V", Produtora = " Rockstar", Preco = 190}},
            { Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa5"), new Jogo{ Id = Guid.Parse("1fa85f64-5717-4562-b3fc-2c963f66afa5"), Nome = "Hollow Knight", Produtora = " Team Cherry", Preco = 28}}
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }
        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return Task.FromResult<Jogo>(null);

            return Task.FromResult(jogos[id]);
        }
        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }
        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach (var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }
            return Task.FromResult(retorno);
        }
        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }
        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }
        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            // Fechar conexão com o banco de dados
        }
    }
}
