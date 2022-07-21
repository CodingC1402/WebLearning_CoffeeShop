import React, { useRef, useState } from 'react';
import Modal from './Modal';
import './Table.css';

type AlignType = 'center' | 'start' | 'end';

type Props = {
    displayProps: string[];
    displayNames: string[];
    indexWidth: string;
    commandWidth: string;
    displayWidths: number[];
    maxHeight: string;
    alignHead: AlignType;
    alignData: AlignType;
    justifyHead: AlignType;
    justifyData: AlignType;
    // If null then ignore if not null then value will be the value of props and return any types
    displayConverters: (((value: any) => any) | null)[];
    borderRadius: number;
    data: any[];
    onDelete?: (obj: any) => void;
    onUpdate?: (obj: any) => void;
}

const Table = (props: Props) => {
    const [showModal, setShowModal] = useState(false);
    const [deletingObj, setDeletingObj] = useState<any>(null);

    const createHeadRow = () => {
        return <tr id='table-head' style={{
            alignItems: props.alignHead,
            borderRadius: `${props.borderRadius}px ${props.borderRadius}px 0px 0px`
        }}>
            <th key={'index'} style={{
                textAlign: props.justifyHead,
                alignItems: props.alignHead,
                width: props.indexWidth,
            }}>#</th>

            {props.displayNames.map((name, index) =>
                <th key={name} style={{
                    textAlign: props.alignHead,
                    flexGrow: props.displayWidths[index],
                    flexBasis: props.indexWidth
                }}>
                    {name}
                </th>)
            }

            <th key={'commands'} style={{width: props.commandWidth, alignItems: props.alignHead}}>Commands</th>
        </tr>
    }

    const createRow = (obj: any, index: number) => {
        return <tr key={index} className='table-row'>
            <td key={'index'} style={{
                textAlign: props.justifyData,
                alignItems: props.alignData,
                width: props.indexWidth,
                fontWeight: 'bold'
            }}>{obj['id'] ? obj['id'] : index}</td>

            {props.displayProps.map((v, i) => 
                <td key={`obj-${v}`} style={{
                    textAlign: props.justifyData,
                    alignItems: props.alignData,
                    flexGrow: props.displayWidths[i],
                    flexBasis: props.indexWidth
                    }}>
                    {props.displayConverters[i] ? props.displayConverters[i]!(obj[v]).toString() : obj[v].toString()}
                </td>
            )}

            <td key={'commands'} id='commands-td' style={{width: props.commandWidth, alignItems: props.alignData}}>
                {props.onUpdate ? <button type='button' className='btn btn-warning command-btn' style={{borderRadius: `${props.borderRadius / 2}px`}} onClick={() => {
                    props.onUpdate!(props.data[index]);
                }}>
                    <i className='bi bi-pen-fill'></i>
                </button> : undefined}
                {props.onDelete ? <button type='button' className='btn btn-danger command-btn' style={{borderRadius: `${props.borderRadius / 2}px`}} onClick={() => {
                    setDeletingObj(props.data[index])
                    setShowModal(true);
                }}>
                    <i className='bi bi-trash-fill'></i>
                </button> : undefined}
            </td>
        </tr>
    }

    return (
        <div id='top-div' style={{borderRadius: `${props.borderRadius}px`}}>
            <table key={'table-head'} className='table'>
                <thead>{createHeadRow()}</thead>
            </table>
            <div key={'table-body'} id='table-body' style={{maxHeight: props.maxHeight, minHeight: props.maxHeight}}>
                <table className='table table-bordered p-0 m-0 table-striped table-dark'>
                    <tbody className='w-100'>
                        {props.data.map(createRow)}
                    </tbody>
                </table>
            </div>
            <div key={'table-footer'} id='table-footer' style={{
                height: `${props.borderRadius}px`,
                borderRadius: `0px 0px ${props.borderRadius}px ${props.borderRadius}px`
            }}/>

            <Modal onConfirm={() => {
                props.onDelete!(deletingObj);
                setDeletingObj(null);
            }} onClose={() => {
                setShowModal(false)
            }} show={showModal}>
                Are you sure you want to delete this row from the table?
            </Modal>
        </div>
    )
}

export default Table