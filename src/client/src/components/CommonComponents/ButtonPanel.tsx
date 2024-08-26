import globalStyles from "./Common.module.css";
import SaveIcon from "@mui/icons-material/Save";

type ButtonPanelProps = {
  OnSave: () => void;
};

const ButtonPanel = (props: ButtonPanelProps) => {
  const handleOnSaveClick = () => {
    props.OnSave();
  };
  return (
    <div className={globalStyles.buttonPanel}>
      <button className={globalStyles.customButton} onClick={handleOnSaveClick}>
        <SaveIcon />
      </button>
      {/* <button className={globalStyles.customButtonEdit}>
        <ModeEditIcon />
      </button> */}
    </div>
  );
};

export default ButtonPanel;
