import ReactDOM from 'react-dom/client'
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App.tsx'
import { Provider } from 'react-redux'
import store from './global/store.ts';

ReactDOM
.createRoot(document.getElementById('root')!)
.render(
    <Provider store={store}>
        <App/>
    </Provider>
);
