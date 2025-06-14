import React, { memo } from "react";
import styles from './InsertPreview.module.css';

interface InsertPreviewProps {
  fullWidth?: boolean;
}

const InsertPreview: React.FC<InsertPreviewProps> = memo(({ fullWidth = false }) => {
  return (
    <div className={`${styles.insertPreview} ${fullWidth ? styles.fullWidth : ''}`}>
      <div className={styles.plusSign}>+</div>
    </div>
  );
});

export default InsertPreview;