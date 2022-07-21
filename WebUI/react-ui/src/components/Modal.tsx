import { useEffect, useState } from "react";
import "./Modal.css";

// If on submit is provided then the modal will ignore on confirm and give the handling of on submit
// to the form instead.
export type ModalProps = {
  onConfirm?: () => void;
  onClose: () => void;
  onSubmit?: (data: any) => void;
	useFormHook?: any;
  show: boolean;
  title: string | React.ReactNode;
	width?: string;
  confirmBtnText?: string;
  cancelBtnText?: string;
  buttonType?: "submit" | "button" | "reset";
  children?: React.ReactNode;
};

const Modal = (props: ModalProps) => {
  const [showModal, setShowModal] = useState(false);
  const [modalFadeOut, setModalFadeOut] = useState(false);
  const {
    handleSubmit,
  } = props.useFormHook || {handleSubmit: undefined};

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
  };

	const submitForm = (data: any) => {
		props.onSubmit!(data);
		closeModal();
	}
  return (
    <form
      className={`modal ${!modalFadeOut ? "modal-fade-in" : "modal-fade-out"}`}
      style={{ display: showModal ? "block" : "none" }}
      id="exampleModalCenter"
      tabIndex={-1}
      role="dialog"
      aria-labelledby="exampleModalCenterTitle"
      aria-hidden="true"
			onSubmit={props.onSubmit ? handleSubmit(submitForm) : undefined}
    >
      <div className="modal-dialog modal-dialog-centered" role="document" style={{width: props.width, maxWidth: props.width}}>
        <div className="modal-content">
          <div className="modal-header">
            <h5
              className="modal-title user-select-none"
              id="exampleModalLongTitle"
            >
              {props.title}
            </h5>
            <button
              type="button"
              className="btn border-1 close"
              aria-label="Close"
							onClick={closeModal}
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div className="modal-body">{props.children}</div>
          <div className="modal-footer">
            <button
              type={props.buttonType || "button"}
              className="btn btn-primary"
              onClick={props.onSubmit ? undefined : () => {
                if (props.onConfirm) props.onConfirm();
                closeModal();
              }}
            >
              {props.confirmBtnText || "Confirm"}
            </button>
            <button
              type="button"
              className="btn btn-secondary"
              onClick={closeModal}
            >
              {props.cancelBtnText || "Cancel"}
            </button>
          </div>
        </div>
      </div>
    </form>
  );
};

export default Modal;
