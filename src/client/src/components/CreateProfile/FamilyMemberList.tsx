import axios from "axios";
import FamilyMember from "./FamilyMember";
import localStyles from "./FamilyMemberList.module.css";
import { baseURL } from "../../features/Helpers/helper";
import { useEffect, useState } from "react";
import { FamilyMembersType } from "../../features/Types/CustomerProfile/CustomerProfileType";

type FamilyMemberListProps = {
  CustomerProfile: string;
  ParentId: string;
  SystemUserId: string;
  handleOnClickMember: (customerId: string) => void;
};

const FamilyMemberList = (props: FamilyMemberListProps) => {
  const { CustomerProfile, ParentId, SystemUserId, handleOnClickMember } =
    props;

  const [familyMembers, setFamilyMembers] = useState<FamilyMembersType[]>([]);

  useEffect(() => {
    console.log(ParentId);
    getFamilyMembers();
  }, [CustomerProfile]);

  const getFamilyMembers = () => {
    axios
      .post<FamilyMembersType[]>(`${baseURL}/getFamilyMembers`, {
        SystemUserId: SystemUserId,
        CustomerProfileId: CustomerProfile,
      })
      .then((response) => {
        if (response.data.length > 0) {
          setFamilyMembers(response.data);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <div className={localStyles.FamilyMemberList}>
      {familyMembers.length > 0 && (
        <>
          <span className={localStyles.SubHeading}>Family Members</span>
          {familyMembers.map((member) => (
            <FamilyMember
              key={member.customerId}
              MemberName={member.lastName + " " + member.otherNames}
              MemberId={member.customerId}
              IsCurrent={CustomerProfile == member.customerId}
              IsPrimary={member.isParent}
              PassportNumber={member.passportNumber}
              ParentId={ParentId}
              handleOnClickMember={handleOnClickMember}
            />
          ))}
        </>
      )}
    </div>
  );
};

export default FamilyMemberList;
