import React from "react";

interface Props extends React.ComponentProps<'input'> {
    label: string;
}

function Input({ label, ...rest }: Props) {
    return (
        <div className="col-6">
            <label>{ label }</label>
            <input className="form-control form-control-lg border-black" {...rest}/>
        </div>
    );
}

export default Input;