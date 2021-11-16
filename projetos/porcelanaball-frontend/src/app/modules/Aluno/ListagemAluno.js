import React, { useState, useEffect } from "react";
import { Card, CardBody, CardHeader } from "../../../_partials/controls";
import { useHistory } from "react-router-dom";
import { TableSearch } from "../../../_helpers/TableSearch";
import  PaginationHelper  from "../../../_helpers/PaginationHelper";
import alunoService from "../../../services/aluno/AlunoService";
import {Table} from "react-bootstrap";
import { Form } from "react-bootstrap";

function ListagemAluno() {
    const history = useHistory();
    const [alunos, setAlunos] = useState([]);
    const [isLoading, setLoading] = useState(false);
    const [searchVal, setSearchVal] = useState(null);
    const { filteredData, loadingSearch } = TableSearch({
      searchVal,
      retrieve: alunos
    });
    const [paginationData, setPaginationData] = useState([]);
    const [alunosSelecionados, setAlunosSelecionados] = useState([]);
    const [teste, setTeste] = useState(false);

    useEffect(() => {
        setLoading(true);
        const promisse = alunoService.getAluno(history);
        promisse.then(function (result) {
            if(result != null){
                setAlunos(result.data);
                setLoading(false);
            }
        });

    }, []);

    function onChangePage(paginationData) {
        setPaginationData(paginationData);
    }

    const handleChange = (event, aluno) => {
        console.log(aluno);
        if(event.target.checked){
            setAlunosSelecionados([...alunosSelecionados,aluno]);
        }
        console.log(alunosSelecionados);
    }

    if (isLoading || loadingSearch) {
        return <div className="d-flex flex-wrap justify-content-between align-items-center">
          <span className="ml-3 spinner spinner-white"></span>
        </div>
      }

    return (
        <>
            <div className="row">
                <div className="col-md-12">
                    <Card className="mt-4">
                        <CardBody>
                        <input type="text" style={{width:'20%'}} className="form-control" placeholder="Pesquisar" onChange={(e) => setSearchVal(e.target.value)} />
                                <Table striped bordered hover>
                                    <thead>
                                        <tr>
                                            <th style={{ width: '0.05%' }}></th>
                                            <th style={{ width: '20%' }}>Nome</th>
                                            <th style={{ width: '20%' }}>Apelido</th>
                                            <th style={{ width: '10%' }}>CPF</th>
                                            <th style={{ width: '5%' }}>Ativo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {paginationData && paginationData.map(aluno =>
                                            <tr key={aluno.codigo}>
                                                <td><Form.Check type="checkbox" onChange={(e) => handleChange(e, aluno)} /></td>
                                                <td>{aluno.nome}</td>
                                                <td>{aluno.apelido}</td>
                                                <td>{aluno.cpf}</td>
                                                <td>{aluno.ativo ? "Sim" : "Não"}</td>
                                            </tr>
                                        )}
                                        {filteredData && !filteredData.length &&
                                            <tr>
                                                <td colSpan="4" className="text-center">
                                                    <div className="p-2">Nenhum aluno encontrado</div>
                                                </td>
                                            </tr>
                                        }
                                    </tbody>
                                </Table>
                                <PaginationHelper
                                    items={filteredData} onChangePage={onChangePage} 
                                />
                            <div className="d-flex flex-row py-4 justify-content-end">
                                
                            </div>
                        </CardBody>
                    </Card>
                </div>
            </div>
        </>
    );
}

export { ListagemAluno };





