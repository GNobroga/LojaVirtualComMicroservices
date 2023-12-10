import React from 'react';
import { RootState } from "../../global/store";
import { useSelector } from "react-redux";
import { useDispatch } from 'react-redux';
import { deleteProduct, findAllProducts } from '../../global/reduces/product';
import { useNavigate } from 'react-router-dom';

function Products() {
    const products = useSelector((selector: RootState) => selector.products);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    React.useEffect(() => {
        dispatch(findAllProducts() as any);
    }, []);

    const deleteProductById = async (id: number) => {
      await dispatch(deleteProduct(id) as any);
    }

    const edit = (productId: number) => {
     navigate(`/create-product?productId=${productId}`);
    };
    
    return (
        <div className="container d-flex flex-column gap-4 p-4">
          <div className="d-flex gap-4 align-items-center bg-dark text-white p-3 rounded">
            <h1 className="display-5 fs-1">Products</h1>
            <button className="btn btn-success" onClick={() => navigate('/create-product')}>Criar um Produto</button>
          </div>
          <ul className="row row-cols-3 gap-4">
              { products.map(p => 
                <li key={p.id} className="list-group-item bg-dark text-white rounded p-5 shadow">
                  <div className="d-flex flex-column gap-2">
                      <h1 className="display-4 fs-2">Name: { p.name }</h1>
                      <p className="text-success">Price: { p.price.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL'}) }</p>
                      <p>Stock: { p.stock }</p>
                      <p>Description: { p.description ?? 'Sem descrição' }</p>
                      <p className="text-danger">Categoria: { p.categoriaName }</p>
                      <div className="d-flex gap-3">
                        <button className="btn btn-warning" onClick={() => edit(p.id)}>Editar</button>
                        <button className="btn btn-danger" onClick={() => deleteProductById(p.id)}>Remover</button>
                      </div>
                  </div>
                </li>  
              )  }
          </ul>
          { !products.length && <p className="text-center display-5 fs-2">Nenhum produto</p>}
        </div>
    );
}

export default Products;