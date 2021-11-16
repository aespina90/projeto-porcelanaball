﻿using PB.Domain;
using PB.Domain.Interface.Repository;
using PB.Domain.Notifications;
using PB.Service.Interface;
using System;
using System.Collections.Generic;
using PB.Utils;

namespace PB.Service
{
    public class PlanoAlunoService : IPlanoAlunoService
    {
        private readonly IPlanoAlunoRepository _repository;
        private readonly NotificationContext _notificationContext;
        private readonly IAlunoRepository _repositoryAluno;
        private readonly IPlanoRepository _repositoryPlano;

        public PlanoAlunoService(
            IPlanoAlunoRepository repository, 
            NotificationContext notificationContext,
            IAlunoRepository repositoryAluno,
            IPlanoRepository repositoryPlano)
        {
            _repository = repository;
            _notificationContext = notificationContext;
            _repositoryAluno = repositoryAluno;
            _repositoryPlano = repositoryPlano;
        }

        public List<PlanoAluno> Get()
        {
            try
            {
                Log.write(Log.Nivel.INFO, "<List> IN");
                List<PlanoAluno> alunoPossuiPlanos = _repository.Get();
                Log.write(Log.Nivel.INFO, "<List> OUT");
                return alunoPossuiPlanos;
            }
            catch (Exception ex)
            {
                Log.write(Log.Nivel.ERROR, ex, "<List> OUT ERROR");
                _notificationContext.AddNotification("Não foi possivel capturar as informações.");
            }

            return null;
        }

        public PlanoAluno Get(int codigo)
        {
            try
            {
                Log.write(Log.Nivel.INFO, "Codigo = " + codigo + " IN");
                PlanoAluno alunoPossuiPlano = _repository.SelectById(codigo);
                Log.write(Log.Nivel.INFO, "Codigo = " + codigo + " OUT");
                return alunoPossuiPlano;
            }
            catch (Exception ex)
            {
                Log.write(Log.Nivel.ERROR, ex, "Codigo = " + codigo + " OUT ERROR");
                _notificationContext.AddNotification("Não foi possivel capturar as informações.");
            }

            return null;
        }

        public int Insert(PlanoAluno alunoPossuiPlano)
        {
            try
            {
                Log.write(Log.Nivel.INFO, "IN");
                if (CheckInsertUpdate(alunoPossuiPlano))
                {
                    int codigoAlunoPossuiPlanoInserido = _repository.Insert(alunoPossuiPlano);
                    Log.write(Log.Nivel.INFO, "OUT");
                    return codigoAlunoPossuiPlanoInserido;
                }
                else
                {
                    Log.write(Log.Nivel.INFO, "Aluno ou Plano inexistente(s) ou inativo(s) OUT");
                    _notificationContext.AddNotification("Aluno ou Plano inexistente(s) ou inativo(s).");
                }
            }
            catch (Exception ex)
            {
                Log.write(Log.Nivel.ERROR, ex, "OUT ERROR");
                _notificationContext.AddNotification("Não foi possivel inserir.");
            }
            return 0;
        }

        public int Update(PlanoAluno alunoPossuiPlano)
        {
            try
            {
                Log.write(Log.Nivel.INFO, "IN");
                if (CheckInsertUpdate(alunoPossuiPlano))
                {
                    _repository.Update(alunoPossuiPlano);
                    Log.write(Log.Nivel.INFO, "OUT");
                }
                else
                {
                    Log.write(Log.Nivel.INFO, "Aluno ou Plano inexistente(s) ou inativo(s) OUT");
                    _notificationContext.AddNotification("Aluno ou Plano inexistente(s) ou inativo(s).");
                }
            }
            catch (Exception ex)
            {
                Log.write(Log.Nivel.ERROR, ex, "OUT ERROR");
                _notificationContext.AddNotification("Não foi possivel alterar.");
            }

            return 0;
        }

        public int Delete(int codigo)
        {
            try
            {
                Log.write(Log.Nivel.INFO, "Codigo  = " + codigo + " IN");
                PlanoAluno alunoPossuiPlano = _repository.SelectById(codigo);

                if (alunoPossuiPlano == null)
                {
                    Log.write(Log.Nivel.INFO, "Codigo  = " + codigo + " nao encontrado OUT");
                    _notificationContext.AddNotification("Este cadastro não foi encontrado no banco de dados.");
                    return 0;
                }

                _repository.Delete(alunoPossuiPlano);
                Log.write(Log.Nivel.INFO, "Codigo  = " + codigo + " OUT");
            }
            catch (Exception ex)
            {
                Log.write(Log.Nivel.ERROR, ex, "Codigo  = " + codigo + " OUT ERROR");
                _notificationContext.AddNotification("Não foi possivel deletar.");
            }

            return 0;
        }

        private bool CheckInsertUpdate(PlanoAluno alunoPossuiPlano)
        {
            try
            {
                Aluno alunoExiste = _repositoryAluno.SelectById(alunoPossuiPlano.aluno_codigo);
                Plano planoExiste = _repositoryPlano.SelectById(alunoPossuiPlano.plano_codigo);

                return ((alunoExiste != null && alunoExiste.ativo) && (planoExiste != null && planoExiste.ativo));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
