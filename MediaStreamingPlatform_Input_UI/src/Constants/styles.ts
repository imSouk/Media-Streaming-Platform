import { type CSSProperties } from 'react';

export const CARD_STYLES: CSSProperties = {
  width: 300,
  minHeight: 200
};

export const CREATE_CARD_STYLES: CSSProperties = {
  ...CARD_STYLES,
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  border: '2px dashed #d9d9d9'
};

export const CONTAINER_STYLES: CSSProperties = {
  display: "flex",
  flexWrap: "wrap",
  gap: "16px",
  padding: "20px",
};

export const ERROR_STYLES: CSSProperties = {
  color: 'red',
  marginTop: '8px'
};