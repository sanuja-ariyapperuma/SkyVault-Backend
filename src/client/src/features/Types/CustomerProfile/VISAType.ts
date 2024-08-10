export type VISAType = {
  vISANumber: string;
  countryId: string;
  vISAIssuedPlace: string;
  vISAIssuedDate: Date;
  vISAExpiryDate: Date;
  assignedToPrimaryPassport: boolean;
  isExpired: boolean;
};
