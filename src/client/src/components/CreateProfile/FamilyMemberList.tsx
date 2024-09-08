import FamilyMember from "./FamilyMember";
import localStyles from "./FamilyMemberList.module.css";
import { useEffect, useState } from "react";
import { FamilyMembersType } from "../../features/Types/CustomerProfile/CustomerProfileType";
import { getFamilyMembersAPI } from "../../features/services/CustomerProfile/apiMethods";

type FamilyMemberListProps = {
  CustomerProfile: string;
  ParentId: string;
  SystemUserId: string;
  handleOnClickMember: (customerId: string) => void;
};

const FamilyMemberList = (props: FamilyMemberListProps) => {
  const { CustomerProfile, ParentId, handleOnClickMember } = props;

  const [familyMembers, setFamilyMembers] = useState<FamilyMembersType[]>([]);

  useEffect(() => {
    console.log(ParentId);
    getFamilyMembers();
  }, [CustomerProfile]);

  const getFamilyMembers = () => {
    getFamilyMembersAPI(CustomerProfile)
      .then((response) => {
        if (response.length > 1) {
          setFamilyMembers(response);
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
