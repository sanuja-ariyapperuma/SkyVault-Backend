import { Button } from "@mui/material";
import PersonOutlineIcon from "@mui/icons-material/PersonOutline";

type FamilyMemberProps = {
  MemberName: string;
  IsCurrent: boolean;
  IsPrimary: boolean;
  PassportNumber: string;
  ParentId: string;
  MemberId: string;
  handleOnClickMember(customerId: string): void;
};

const FamilyMember = (prop: FamilyMemberProps) => {
  const {
    MemberName,
    IsCurrent,
    IsPrimary,
    PassportNumber,
    MemberId,
    handleOnClickMember,
  } = prop;

  return (
    <Button
      variant="outlined"
      sx={{
        justifyContent: "flex-start",
        margin: 0.5,
        padding: 2,
        fontWeight: IsCurrent ? "bold" : "normal",
      }}
      startIcon={<PersonOutlineIcon />}
      fullWidth={true}
      onClick={() => handleOnClickMember(MemberId)}
    >
      {MemberName} {IsPrimary ? "(Primary)" : ""} | {PassportNumber}
    </Button>
  );
};

export default FamilyMember;
