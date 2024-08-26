import { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import localStyles from "../components/CreateProfile/CustomerProfile.module.css";
import { OptionsType } from "../features/Types/Dashboard/dashboardTypes";
import { useConfirmDialog } from "../hooks/useConfirmDialog";
import ConfirmBox from "../components/CommonComponents/ConfirmBox";
import FrequentFlyNumberAccordion from "../components/CreateProfile/FrequentFlyNumberAccordion";
import ComMethodAccordion from "../components/CreateProfile/ComMethodAccordion";
import PassportAccordion from "../components/CreateProfile/PassportAccordion";
import VISAAccordion from "../components/CreateProfile/VISAAccordion";
import AddIcon from "@mui/icons-material/Add";

import globalStyles from "../components/CommonComponents/Common.module.css";
import FamilyMemberList from "../components/CreateProfile/FamilyMemberList";

const CustomerProfile = () => {
  let { profileId } = useParams();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    if (location.state) {
      setParentId(location.state.parentId || "");
      setCustomerProfileId(location.state.customerProfileId || "");
      setPrimaryPassportId(location.state.primaryPassportId || "");
      setSecondaryPassportId(location.state.secondaryPassportId || "");
    } else if (profileId) {
      setCustomerProfileId(profileId);
    }
  }, [location.state]);

  const [country, setCountry] = useState<OptionsType[]>([]);
  const { dialogProps, openDialog } = useConfirmDialog();
  const [parentId, setParentId] = useState<string | null>("");
  const [customerProfileId, setCustomerProfileId] = useState<string | null>("");
  const [primaryPassportId, setPrimaryPassportId] = useState<string | null>("");
  const [secondaryPassportId, setSecondaryPassportId] = useState<string | null>(
    ""
  );

  const setPrimaryPassportSavedId = (id: string) => {
    setPrimaryPassportId(id);
  };
  const setSecondaryPassportSavedId = (id: string) => {
    setSecondaryPassportId(id);
  };

  const handleOnAddingFamilyMember = () => {
    navigate("/customer-profile", {
      state: {
        parentId: customerProfileId,
        customerProfileId: "",
        primaryPassportId: "",
        secondaryPassportId: "",
      },
    });
  };

  const handleOnClickMember = (customerId: string) => {
    setCustomerProfileId(customerId.toString());
  };

  return (
    <div className={localStyles.profileManagementContainer}>
      <ConfirmBox {...dialogProps} />
      <div className={localStyles.topHeading}>
        <h2 className={localStyles.pageHeader}>
          {profileId ? "Update Profile" : "Create Profile"}
        </h2>
      </div>
      <div className={localStyles.buttonContainer}>
        {customerProfileId && (
          <button
            className={globalStyles.customButton}
            onClick={handleOnAddingFamilyMember}
          >
            <AddIcon className={localStyles.buttonIcon} />
            Add Family Member
          </button>
        )}
      </div>
      <br />
      {customerProfileId ? (
        <>
          <FamilyMemberList
            CustomerProfile={customerProfileId ?? ""}
            ParentId={parentId ?? ""}
            SystemUserId="9"
            handleOnClickMember={handleOnClickMember}
          />
          <br />
        </>
      ) : (
        <></>
      )}

      <PassportAccordion
        setCustomerProfileId={setCustomerProfileId}
        customerProfileId={customerProfileId ?? ""}
        setCountry={setCountry}
        country={country}
        setPrimaryPassportSavedId={setPrimaryPassportSavedId}
        setSecondaryPassportSavedId={setSecondaryPassportSavedId}
      />
      <VISAAccordion
        customerProfileId={customerProfileId ?? ""}
        primaryPassportId={primaryPassportId ?? ""}
        secondaryPassportId={secondaryPassportId ?? ""}
        country={country}
        openDialog={openDialog}
      />
      <FrequentFlyNumberAccordion
        CustomerProfileId={customerProfileId ?? ""}
        SystemUser={"9"}
      />
      <ComMethodAccordion
        CustomerProfileId={customerProfileId ?? ""}
        SystemUser="9"
      />
      {/* <div className={localStyles.footerButtonArea}>
        <ButtonPanel OnSave={handleOnSavePrimaryPassport} />
      </div> */}
    </div>
  );
};

export default CustomerProfile;
