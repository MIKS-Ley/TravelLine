import React, { useCallback, useRef } from 'react';
import type { Task } from '../../types/taskTypes';
import styles from './TaskItem.module.css';

interface TaskItemProps {
  task: Task;
  isDragged?: boolean;
  onDragStart: (
    task: Task,
    clientX: number,
    clientY: number,
    offsetX: number,
    offsetY: number
  ) => void;
  taskRef: (el: HTMLDivElement | null) => void;
}

const TaskItem: React.FC<TaskItemProps> = ({ 
  task, 
  isDragged = false, 
  onDragStart,
  taskRef
}) => {
  const taskRefInternal = useRef<HTMLDivElement>(null);

  const handleMouseDown = useCallback((e: React.MouseEvent) => {
    const el = taskRefInternal.current;
    if (!el) return;
    const rect = el.getBoundingClientRect();
    const offsetX = e.clientX - rect.left;
    const offsetY = e.clientY - rect.top;
    onDragStart(task, e.clientX, e.clientY, offsetX, offsetY);
  }, [onDragStart, task]);

  return (
    <div
      ref={(el) => {
        taskRefInternal.current = el;
        taskRef(el);
      }}
      className={`${styles.task} ${isDragged ? styles.dragged : ''}`}
      onMouseDown={handleMouseDown}
    >
      {task.content}
    </div>
  );
};

export default TaskItem;