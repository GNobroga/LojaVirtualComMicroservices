import React from "react";
import Input from "../input/Input";
import { createProduct, findById, getCategories, updateProduct } from "../../global/reduces/product";
import { useDispatch } from "react-redux";
import {  useLocation, useNavigate, useParams } from "react-router-dom";
import { Product } from "../../models/Product";
import { Category } from "../../models/Category";

function CreateProduct() {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const location = useLocation();

    const [edition, setEdition] = React.useState(false);
    const [categories, setCategories] = React.useState([] as Category[]);
    const [product, setProduct] = React.useState({
        id: 0,
        name: '',
        price: 0,
        description: '',
        stock: 0,
        categoryId: 0,
        imageUrl: null
    } as Product);

    React.useEffect(() => {
        dispatch(getCategories() as any)
            .then((categories: Category[]) => {
                setCategories(categories);
                if (categories.length && product.categoryId === 0) {
                    setProduct({ ...product, categoryId: categories[0].id });
                }
            });
    }, []);

    React.useEffect(() => {
        const params = new URLSearchParams(location.search);
        const productId = parseInt(params.get('productId') ?? '');
        if (!Number.isNaN(productId)) {
            dispatch(findById(productId) as any)
                .then((prod: Product) => {
                    setProduct({ ...prod });
                    setEdition(true);
                })
                .catch((error: any) => window.alert("ERROR AO EDITAR"));
        }
        
    }, [location.search]);

    const onChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const name = e.target.name as keyof Product;
        const type = e.target.type;

        if (type === 'number' || e.target instanceof HTMLSelectElement) {
            setProduct({...product, [name]: parseInt(e.target.value) });
        }
        else {
            setProduct({...product, [name]: e.target.value });
        }
    }

    const create = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        await dispatch(createProduct(product) as any);
        navigate('/');
    };

    const edit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        await dispatch(updateProduct(product) as any);
        navigate('/');
       
    }

    return (
        <form className="col-6 mx-auto p-5 d-flex gap-3 flex-column">
           <div className="row">
                <Input label="Name" name="name" type="text" value={product.name} onChange={onChange}/>
                <Input label="Price" name="price" type="number" value={product.price} onChange={onChange}/>
           </div>
            <div className="row">
                <Input label="Description" name="description" value={product.description ?? ''} type="text" onChange={onChange}/>
                <Input label="Stock" name="stock" type="number" value={product.stock} onChange={onChange}/>
            </div>
           <div className="row align-items-center">
                <Input label="Image URL" name="imageUrl" value={product.imageUrl ?? ''} type="text" onChange={onChange}/>
                <div className="col">
                    <label>Categorias</label>
                    <select className="form-select border border-dark" value={product.categoryId} name="categoryId" onChange={onChange}>
                        { categories.map(c => <option key={c.id} value={c.id}>{ c.name }</option>)}
                    </select>
                </div>
           </div>
            <button type="button" onClick={edition ? edit : create} className="btn btn-lg btn-success">{ edition ? 'Editar' : 'Cadastrar'}</button>
        </form>
    );
}

export default CreateProduct;