import { CommunicationMethod } from "./CommunicationMethodEnum";
import { PassportType } from "./PassportType";
import { VISAType } from "./VISAType";

export type CustomerProfileType = {
  PrimaryPassport: PassportType;
  SecondaryPassport: PassportType | null | undefined;
  VISAs: VISAType[] | null | undefined;
  FrequentFlyerNumber: string[];
  PreferredCommunicationMethod: CommunicationMethod;
};

export type FamilyMembersType = {
  customerId: string;
  lastName: string;
  otherNames: string;
  passportNumber: string;
  isParent: boolean;
};
