import React, { memo } from 'react';
import type { Task } from '../../types/taskTypes';
import styles from './DragPreview.module.css';

interface DragPreviewProps {
  x: number;
  y: number;
  task: Task;
}

const DragPreview: React.FC<DragPreviewProps> = memo(({ x, y, task }) => {
  return (
    <div
      className={styles.preview}
      style={{
        top: y,
        left: x,
      }}
    >
      {task.content}
    </div>
  );
});

export default DragPreview;