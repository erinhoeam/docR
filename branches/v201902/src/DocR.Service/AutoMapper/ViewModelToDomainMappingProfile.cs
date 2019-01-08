using AutoMapper;

namespace DocR.Service.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //CreateMap<ProfessorViewModel, InserirProfessorCommand>()
            //    .ConstructUsing(c => new InserirProfessorCommand(c.Id, c.Nome, c.NomePai, c.NomeMae, c.Endereco, c.Bairro, c.Cidade, c.Estado,c.Cep, 
            //        c.DataNascimento, c.Sexo, c.EstadoCivil, c.Nacionalidade, c.Naturalidade, c.Rg, c.Cpf, c.Pis, c.TituloEleitor, c.Contatos,
            //        c.Cargo, c.Readaptado, c.Matricula, c.Classe, c.Nivel, c.CargaHoraria, c.DataAdmissao, c.Vinculo, c.Lotacao, c.OutroVinculoEmpregaticio, 
            //        c.RegimeDoOutroVinculo, c.SecretariaDeEducacao, c.GestaoEscolar, c.AreaDeAtuacao,
            //        c.EnsinoFundamentalAnosFinais, c.EnsinoMedio, c.Graduacao, c.TemEspecializacao, c.Especializacao, c.TemMestrado, c.Mestrado, 
            //        c.TemDoutorado, c.Doutorado, c.Imagem));

            //CreateMap<ProfessorViewModel, AtualizarProfessorCommand>()
            //    .ConstructUsing(c => new AtualizarProfessorCommand(c.Id, c.Nome, c.NomePai, c.NomeMae, c.Endereco, c.Bairro, c.Cidade, c.Estado, c.Cep,
            //        c.DataNascimento, c.Sexo, c.EstadoCivil, c.Nacionalidade, c.Naturalidade, c.Rg, c.Cpf, c.Pis, c.TituloEleitor, c.Contatos,
            //        c.Cargo, c.Readaptado, c.Matricula, c.Classe, c.Nivel, c.CargaHoraria, c.DataAdmissao, c.Vinculo, c.Lotacao, c.OutroVinculoEmpregaticio,
            //        c.RegimeDoOutroVinculo, c.SecretariaDeEducacao, c.GestaoEscolar, c.AreaDeAtuacao,
            //        c.EnsinoFundamentalAnosFinais, c.EnsinoMedio, c.Graduacao, c.TemEspecializacao, c.Especializacao, c.TemMestrado, c.Mestrado,
            //        c.TemDoutorado, c.Doutorado, c.Imagem));
        }
    }
}