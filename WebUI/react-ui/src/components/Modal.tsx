import { useEffect, useState } from 'react'
import './Modal.css'

type Props = {
    onConfirm: () => void;
    onClose: () => void;
    show: boolean;
    children?: React.ReactNode;
}

const Modal = (props: Props) => {
    const [showModal, setShowModal] = useState(false);
    const [modalFadeOut, setModalFadeOut] = useState(false);

    useEffect(() => {
        setShowModal(props.show);
    }, [props.show]);

    const closeModal = () => {
        setModalFadeOut(true);
        setTimeout(() => {
            setShowModal(false);
            setModalFadeOut(false);
            props.onClose();
        }, 220);
    }

    return (
        <div className={`modal ${!modalFadeOut ? 'modal-fade-in' : 'modal-fade-out'}`} style={{display: showModal ? 'block' : 'none'}} id="exampleModalCenter" tabIndex={-1} role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div className="modal-dialog modal-dialog-centered" role="document">
                <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title user-select-none" id="exampleModalLongTitle">Confirmation</h5>
                    <button type="button" className="btn border-1 close" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div className="modal-body">
                    {props.children}
                </div>
                <div className="modal-footer">
                    <button type="button" className="btn btn-primary" onClick={() => {
                        props.onConfirm();
                        closeModal();
                    }}>Confirm</button>
                    <button type="button" className="btn btn-secondary" onClick={closeModal}>Cancel</button>
                </div>
                </div>
            </div>
        </div>
    )
}

export default Modal