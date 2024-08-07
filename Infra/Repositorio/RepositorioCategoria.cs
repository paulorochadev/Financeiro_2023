﻿using Domain.Interfaces.ICategoria;
using Entities.Entidades;
using Infra.Repositorio.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioCategoria : RepositoryGenerics<Categoria>, InterfaceCategoria
    {
        public Task<IList<Categoria>> ListarCategoriasUusario(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
